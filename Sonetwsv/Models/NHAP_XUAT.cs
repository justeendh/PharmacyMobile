namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class NHAP_XUAT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHAP_XUAT()
        {
            BANG_VON = new HashSet<BANG_VON>();
            PUTIN_OUT = new HashSet<PUTIN_OUT>();
        }

        public Guid? KEY_CHI_NHANH { get; set; }

        public Guid? KEY_CHUNG_TU { get; set; }

        public Guid? KEY_NGUOI_DUNG { get; set; }

        public Guid? KEY_NGUOI_BAN { get; set; }

        public Guid? KEY_KHACH_HANG { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BANG_VON> BANG_VON { get; set; }

        public virtual CHI_NHANH CHI_NHANH { get; set; }

        public virtual CHUNG_TU CHUNG_TU { get; set; }

        public virtual KHACH_HANG KHACH_HANG { get; set; }

        public virtual NGUOI_BAN NGUOI_BAN { get; set; }

        public virtual NGUOI_DUNG NGUOI_DUNG { get; set; }

        public virtual TRANG_THAI TRANG_THAI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PUTIN_OUT> PUTIN_OUT { get; set; }
    }
}
