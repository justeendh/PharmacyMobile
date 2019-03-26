namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class MOVE_HANG
    {
        [Key]
        public Guid KEY_MOVE_HANG { get; set; }

        public Guid? KEY_NHAP_XUAT { get; set; }

        public Guid? KEY_NHANH_DEN { get; set; }

        [StringLength(31)]
        public string MA_NHANH_DEN { get; set; }

        public DateTime? NGAY_MOVE_HANG { get; set; }

        [StringLength(31)]
        public string COD_MOVE_HANG { get; set; }

        public bool? SED_MOVE_HANG { get; set; }

        [StringLength(31)]
        public string COD_PHIEU_GOC { get; set; }

        public short? TYP_MOVE_HANG { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }
    }
}
