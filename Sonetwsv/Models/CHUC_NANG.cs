namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class CHUC_NANG
    {
        [Key]
        [StringLength(255)]
        public string MA_CHUC_NANG { get; set; }

        [StringLength(255)]
        public string TEN_CHUC_NANG { get; set; }

        [StringLength(255)]
        public string CHA_CHUC_NANG { get; set; }
    }
}
