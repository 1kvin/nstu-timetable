using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nstu_timetable.DbContexts
{
    public partial class TeacherContact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TeacherId { get; set; }
        
        [MaxLength(50)]
        public string Type { get; set; } = null!;
        
        [MaxLength(500)]
        public string Value { get; set; } = null!;
    }
}
