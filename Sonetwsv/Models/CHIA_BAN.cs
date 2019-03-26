namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class CHIA_BAN
    {
        [Key]
        [StringLength(31)]
        public string MA_CHIA_BAN { get; set; }

        [StringLength(255)]
        public string TEN_CHIA_BAN { get; set; }
    }
}
