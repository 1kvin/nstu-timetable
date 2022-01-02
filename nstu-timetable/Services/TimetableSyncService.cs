using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using nstu_timetable.DbContexts;

namespace nstu_timetable.Services
{
    public class TimetableSyncService
    {
        private readonly NstuTimetableContext nstuTimetableContext;
        private readonly ILogger<GroupSyncService> logger;

        public TimetableSyncService(NstuTimetableContext nstuTimetableContext, ILogger<GroupSyncService> logger)
        {
            this.nstuTimetableContext = nstuTimetableContext;
            this.logger = logger;
        }

        public async Task StartSyncAsync()
        {
            logger.LogInformation("Start sync timetable");

            try
            {
                Parallel.ForEach(nstuTimetableContext.Groups.ToList(), TrySyncGroupTimetable);
                logger.LogInformation("Groups are synchronized");
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error sync groups");
            }
        }

        private void TrySyncGroupTimetable(Group group)
        {
            try
            {
                SyncGroupTimetable(group);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Error sync group {group.GroupName}");
            }
        }

        private void SyncGroupTimetable(Group group)
        {
            var timetable = GetGroupTimeTableFromSite(group.ScheduleUrl);
        }

        private Timetable GetGroupTimeTableFromSite(string url)
        {
            throw new NotImplementedException();
        }
    }
}