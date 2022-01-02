using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace nstu_timetable
{
    public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}