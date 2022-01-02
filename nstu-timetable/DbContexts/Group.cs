using System;
using System.Collections.Generic;

namespace nstu_timetable.DbContexts
{
    public partial class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; } = null!;
        public int CourseNumber { get; set; }
        public string? ScheduleUrl { get; set; }
        public int FacultyId { get; set; }

        public virtual Faculty Faculty { get; set; } = null!;
    }
}
