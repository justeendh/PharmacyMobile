namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class NHIEM_VU
    {
        [Key]
        public Guid KEY_NHIEM_VU { get; set; }

        [StringLength(31)]
        public string MA_NHIEM_VU { get; set; }

        [StringLength(255)]
        public string TEN_NHIEM_VU { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }
    }
}
