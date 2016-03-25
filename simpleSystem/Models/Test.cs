namespace simpleSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Stats.Tests")]
    public partial class Test
    {
        public Test()
        {
            Scores = new HashSet<Score>();
        }

        [StringLength(10)]
        public string TestID { get; set; }

        public virtual ICollection<Score> Scores { get; set; }
    }
}
