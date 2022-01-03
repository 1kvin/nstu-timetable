using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nstu_timetable.DbContexts
{
    public partial class Timetable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        [MaxLength(50)]
        public string Subject { get; set; } = null!;
        
        [MaxLength(20)]
        public string? Classroom { get; set; }
        
        [MaxLength(20)]
        public string? TypeName { get; set; }
        public int? TeacherId { get; set; }
    }
}
