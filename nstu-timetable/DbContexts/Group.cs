using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nstu_timetable.DbContexts
{
    public partial class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [MaxLength(10)]
        public string GroupName { get; set; } = null!;
        public int CourseNumber { get; set; }
        
        [MaxLength(150)]
        public string? ScheduleUrl { get; set; }
        public int FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; } = null!;
    }
}
