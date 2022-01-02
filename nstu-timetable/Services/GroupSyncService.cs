using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using nstu_timetable.DbContexts;
using nstu_timetable.Parsers;

namespace nstu_timetable.Services
{
    public class GroupSyncService
    {
        private const string baseUrl = "https://www.nstu.ru/studies/schedule/schedule_classes";
        private readonly NstuTimetableContext nstuTimetableContext;
        private readonly ILogger<GroupSyncService> logger;

        public GroupSyncService(NstuTimetableContext nstuTimetableContext, ILogger<GroupSyncService> logger)
        {
            this.nstuTimetableContext = nstuTimetableContext;
            this.logger = logger;
        }

        public async Task StartSyncAsync()
        {
            logger.LogInformation("Start sync groups");

            try
            {
                var groups = await GetAllGroupsFromSiteAsync();
                groups.ForEach(SyncGroup);
                logger.LogInformation("Groups are synchronized");
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error sync groups");
            }
        }


        private async Task<List<Group>> GetAllGroupsFromSiteAsync()
        {
            var request = WebRequest.Create(baseUrl);
            request.Method = "GET";
            using (var requestStream = (await request.GetResponseAsync()).GetResponseStream())
            {
                using (var reader = new StreamReader(requestStream))
                {
                    return await GroupParser.ParseAllGroupAsync(await reader.ReadToEndAsync());
                }
            }
        }

        private void SyncGroup(Group group)
        {
            SyncGroupFacultyFromDb(group);
            SyncGroupFromDb(group);

            nstuTimetableContext.SaveChanges();
        }

        private void SyncGroupFromDb(Group group)
        {
            var groupInDb = nstuTimetableContext.Groups.SingleOrDefault(g => g.GroupName == group.GroupName);

            if (groupInDb == null)
            {
                nstuTimetableContext.Groups.Add(group);
                logger.LogInformation($"add new group {group.GroupName}");
            }
            else
            {
                groupInDb.ScheduleUrl = group.ScheduleUrl;
                groupInDb.CourseNumber = group.CourseNumber;
            }
        }

        private void SyncGroupFacultyFromDb(Group group)
        {
            var faculty = group.Faculty;
            var facultyInDb = nstuTimetableContext.Faculties.SingleOrDefault(f =>
                (f.FullName == faculty.FullName) && (f.TypeSubtitle == faculty.TypeSubtitle));

            if (facultyInDb == null)
            {
                nstuTimetableContext.Faculties.Add(group.Faculty);
                logger.LogInformation($"add new faculty {faculty.FullName} {faculty.TypeSubtitle}");
            }
            else
            {
                group.Faculty = facultyInDb;
            }
        }
    }
}