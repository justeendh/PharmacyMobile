namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class UPD_BAN_HANG
    {
        public Guid? KEY_CHI_NHANH { get; set; }

        public Guid? KEY_CHUNG_TU { get; set; }

        public Guid? KEY_NGUOI_DUNG { get; set; }

        [Key]
        public Guid KEY_BAN_HANG { get; set; }

        [StringLength(31)]
        public string KY_HIEU_PHIEU { get; set; }

        [StringLength(31)]
        public string SO_PHIEU_BAN { get; set; }

        public DateTime? NGAY_BAN_HANG { get; set; }

        public DateTime? NGAY_GIO_BAN { get; set; }

        public Guid? KEY_KHACH_HANG { get; set; }

        [StringLength(255)]
        public string TEN_KHACH_HANG { get; set; }

        [StringLength(255)]
        public string DIA_CHI_KHACH { get; set; }

        [StringLength(255)]
        public string LYDO_XUAT_BAN { get; set; }

        public Guid? KEY_NGUOI_BAN { get; set; }

        [StringLength(255)]
        public string NGUOI_KE_DON { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TIEN_MAT_NHAN { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TIEN_THOI_LAI { get; set; }

        [StringLength(31)]
        public string PHAN_CHIA_BAN { get; set; }

        [StringLength(31)]
        public string CODE_BAR_CODE { get; set; }

        public bool? DUYET_THU_TIEN { get; set; }

        [StringLength(1023)]
        public string GHI_BAN_HANG { get; set; }

        public bool? XUAT_HOA_DON { get; set; }

        public int? DIEM_QUI_UOC { get; set; }

        public int? DIEM_QUI_DOI { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TIEN_TRU_DIEM { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }

        public Guid? KEY_GOM_HANG { get; set; }

        public Guid? KEY_THU_NGAN { get; set; }

        [StringLength(255)]
        public string TEN_BENH_NHAN { get; set; }
    }
}
