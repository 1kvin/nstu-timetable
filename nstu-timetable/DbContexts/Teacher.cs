using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nstu_timetable.DbContexts
{
    public partial class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(100)]
        public string FullName { get; set; } = null!;
        
        [MaxLength(20)]
        public string? ShortAcademicDegree { get; set; }
        [MaxLength(300)]
        public string? PhotoUrl { get; set; }
    }
}
