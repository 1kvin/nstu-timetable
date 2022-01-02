using System;
using System.Collections.Generic;

namespace nstu_timetable.DbContexts
{
    public partial class TeacherContact
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public string Type { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}
