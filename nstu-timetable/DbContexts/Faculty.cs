using System;
using System.Collections.Generic;

namespace nstu_timetable.DbContexts
{
    public partial class Faculty
    {
        public Faculty()
        {
            Groups = new HashSet<Group>();
        }

        public int Id { get; set; }
        public string? ShortName { get; set; }
        public string FullName { get; set; } = null!;
        public string TypeSubtitle { get; set; } = null!;

        public virtual ICollection<Group> Groups { get; set; }
    }
}
