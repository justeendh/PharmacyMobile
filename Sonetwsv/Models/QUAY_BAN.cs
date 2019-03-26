namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class QUAY_BAN
    {
        [Key]
        [StringLength(31)]
        public string MA_QUAY_BAN { get; set; }

        [StringLength(255)]
        public string TEN_QUAY_BAN { get; set; }
    }
}
