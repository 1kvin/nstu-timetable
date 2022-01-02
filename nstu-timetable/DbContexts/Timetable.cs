using System;
using System.Collections.Generic;

namespace nstu_timetable.DbContexts
{
    public partial class Timetable
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Subject { get; set; } = null!;
        public string? Classroom { get; set; }
        public string? TypeName { get; set; }
        public int? TeacherId { get; set; }
    }
}
