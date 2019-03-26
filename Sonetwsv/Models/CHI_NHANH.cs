namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class CHI_NHANH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CHI_NHANH()
        {
            BAN_HANG = new HashSet<BAN_HANG>();
            DIEU_NHAP = new HashSet<DIEU_NHAP>();
            KHIEU_NAI = new HashSet<KHIEU_NAI>();
            NHAP_XUAT = new HashSet<NHAP_XUAT>();
            SODU_KHACH = new HashSet<SODU_KHACH>();
            TIEN_GIAO = new HashSet<TIEN_GIAO>();
        }

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

        public int? SRV_RECV_DATA { get; set; }

        public int? KOX_TIME_SEND { get; set; }

        public int? KOX_TIME_RECV { get; set; }

        public int? KOX_TIME_BACK { get; set; }

        public int? KOX_TIME_PEER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BAN_HANG> BAN_HANG { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIEU_NHAP> DIEU_NHAP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KHIEU_NAI> KHIEU_NAI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHAP_XUAT> NHAP_XUAT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SODU_KHACH> SODU_KHACH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TIEN_GIAO> TIEN_GIAO { get; set; }
    }
}
