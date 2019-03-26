namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class UPD_CHI_NHANH
    {
        [Key]
        public Guid KEY_CHI_NHANH { get; set; }

        [StringLength(31)]
        public string MA_CHI_NHANH { get; set; }

        [StringLength(255)]
        public string TEN_CHI_NHANH { get; set; }

        [StringLength(255)]
        public string DIA_CHI_NHANH { get; set; }

        [StringLength(255)]
        public string SO_DIEN_THOAI { get; set; }

        public bool? BAO_CAO_TONG { get; set; }

        public bool? CON_HOAT_DONG { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }
    }
}
