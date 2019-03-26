namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class UPD_NHAP_XUAT
    {
        public Guid? KEY_CHI_NHANH { get; set; }

        public Guid? KEY_CHUNG_TU { get; set; }

        [Key]
        public Guid KEY_NHAP_XUAT { get; set; }

        [StringLength(31)]
        public string KY_HIEU_PHIEU { get; set; }

        [StringLength(31)]
        public string SO_NHAP_XUAT { get; set; }

        [StringLength(31)]
        public string SO_HOA_DON { get; set; }

        [StringLength(31)]
        public string HIEU_HOA_DON { get; set; }

        public DateTime? NGAY_NHAP_XUAT { get; set; }

        public Guid? KEY_NGUOI_DUNG { get; set; }

        public Guid? KEY_NGUOI_BAN { get; set; }

        public Guid? KEY_KHACH_HANG { get; set; }

        [StringLength(255)]
        public string TEN_KHACH_HANG { get; set; }

        [StringLength(255)]
        public string DIA_CHI_KHACH { get; set; }

        [StringLength(255)]
        public string SO_TEL_KHACH { get; set; }

        [StringLength(255)]
        public string LYDO_NHAP_XUAT { get; set; }

        public bool? KHAI_THUE_VAT { get; set; }

        public bool? HOA_DON_KYGUI { get; set; }

        public short? LOAI_KHAI_THUE { get; set; }

        public short? COD_TRANG_THAI { get; set; }

        [StringLength(1023)]
        public string GHI_NHAP_XUAT { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }
    }
}
