namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Stats.Scores")]
    public partial class Score
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string TestID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string StudentID { get; set; }

        [Column("Score")]
        public byte Score1 { get; set; }

        public virtual Test Test { get; set; }
    }
}
