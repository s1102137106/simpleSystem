namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Num
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int n { get; set; }
    }
}
