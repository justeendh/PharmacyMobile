namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class NGUOI_DUNG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NGUOI_DUNG()
        {
            BAN_HANG = new HashSet<BAN_HANG>();
            DIEU_NHAP = new HashSet<DIEU_NHAP>();
            KHIEU_NAI = new HashSet<KHIEU_NAI>();
            NHAP_XUAT = new HashSet<NHAP_XUAT>();
            TIEN_GIAO = new HashSet<TIEN_GIAO>();
        }

        [Key]
        public Guid KEY_NGUOI_DUNG { get; set; }

        [StringLength(31)]
        public string MA_NGUOI_DUNG { get; set; }

        [StringLength(255)]
        public string TEN_NGUOI_DUNG { get; set; }

        [StringLength(255)]
        public string MA_TRUY_CAP { get; set; }

        public short? MA_RIGHT_VIEW { get; set; }

        public bool? CO_SU_DUNG { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }

        [StringLength(31)]
        public string SIG_NGUOI_DUNG { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BAN_HANG> BAN_HANG { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIEU_NHAP> DIEU_NHAP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KHIEU_NAI> KHIEU_NAI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHAP_XUAT> NHAP_XUAT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TIEN_GIAO> TIEN_GIAO { get; set; }
    }
}
