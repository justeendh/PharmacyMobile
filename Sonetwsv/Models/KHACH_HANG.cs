namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class KHACH_HANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHACH_HANG()
        {
            DUNO_KHACH = new HashSet<DUNO_KHACH>();
            KHIEU_NAI = new HashSet<KHIEU_NAI>();
            NHAP_XUAT = new HashSet<NHAP_XUAT>();
            SODU_KHACH = new HashSet<SODU_KHACH>();
        }

        [Key]
        public Guid KEY_KHACH_HANG { get; set; }

        [StringLength(31)]
        public string MA_KHACH_HANG { get; set; }

        [StringLength(255)]
        public string TEN_KHACH_HANG { get; set; }

        [StringLength(255)]
        public string DIA_CHI_KHACH { get; set; }

        [StringLength(255)]
        public string SO_DIEN_THOAI { get; set; }

        [StringLength(31)]
        public string MA_SO_THUE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? HAN_MUC_NO { get; set; }

        [StringLength(255)]
        public string SO_TAI_KHOAN { get; set; }

        [StringLength(255)]
        public string MAT_HANG_CHINH { get; set; }

        [StringLength(255)]
        public string THUOC_CO_QUAN { get; set; }

        [StringLength(255)]
        public string NGUOI_GIAO_DICH { get; set; }

        public DateTime? NGAY_SINH_NHAT { get; set; }

        public bool? CO_GIAO_DICH { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DUNO_KHACH> DUNO_KHACH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KHIEU_NAI> KHIEU_NAI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHAP_XUAT> NHAP_XUAT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SODU_KHACH> SODU_KHACH { get; set; }
    }
}
