using nstu_timetable.DbContexts;
using nstu_timetable.Parsers;

namespace nstu_timetable.Services
{
    public class GroupSyncService
    {
        private const string baseUrl = "https://www.nstu.ru/studies/schedule/schedule_classes";
        private readonly NstuTimetableContext nstuTimetableContext;
        private readonly ILogger<GroupSyncService> logger;

        private readonly ParserWorker<List<Group>> groupParserWorker;

        public GroupSyncService(NstuTimetableContext nstuTimetableContext, ILogger<GroupSyncService> logger)
        {
            groupParserWorker = new ParserWorker<List<Group>>(new GroupParser(), baseUrl);
            this.nstuTimetableContext = nstuTimetableContext;
            this.logger = logger;
        }

        public async Task StartSyncAsync()
        {
            logger.LogInformation("Start sync groups");
            
            try
            {
                var groups = await groupParserWorker.Start();
                groups.ForEach(SyncGroup);
                logger.LogInformation("Groups are synchronized");
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error sync groups");
            }
        }
        

        private void SyncGroup(Group group)
        {
            try
            {
                SyncGroupFacultyFromDb(group);
                SyncGroupFromDb(group);
                nstuTimetableContext.SaveChanges();
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Error sync group {group.GroupName}");
            }
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