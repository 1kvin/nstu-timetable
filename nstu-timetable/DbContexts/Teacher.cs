using System;
using System.Collections.Generic;

namespace nstu_timetable.DbContexts
{
    public partial class Teacher
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? ShortAcademicDegree { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
