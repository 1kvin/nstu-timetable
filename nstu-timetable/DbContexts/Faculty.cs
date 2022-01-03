using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nstu_timetable.DbContexts
{
    public partial class Faculty
    {
        public Faculty()
        {
            Groups = new HashSet<Group>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [MaxLength(15)]
        public string? ShortName { get; set; }
        
        [MaxLength(100)]
        public string FullName { get; set; } = null!;
        
        [MaxLength(100)]
        public string TypeSubtitle { get; set; } = null!;

        public virtual ICollection<Group> Groups { get; set; }
    }
}
