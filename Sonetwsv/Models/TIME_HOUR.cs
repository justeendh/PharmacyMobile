namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class TIME_HOUR
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short COD_TIME_HOUR { get; set; }

        [StringLength(255)]
        public string TEN_TIME_HOUR { get; set; }
    }
}
