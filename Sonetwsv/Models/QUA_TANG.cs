namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class QUA_TANG
    {
        [Key]
        public Guid KEY_QUA_TANG { get; set; }

        [StringLength(255)]
        public string TEN_QUA_TANG { get; set; }

        public decimal? UND_QUA_TANG { get; set; }

        public decimal? OVE_QUA_TANG { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }
    }
}
