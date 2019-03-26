namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class CHUNG_TU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CHUNG_TU()
        {
            BAN_HANG = new HashSet<BAN_HANG>();
            NHAP_XUAT = new HashSet<NHAP_XUAT>();
        }

        [StringLength(31)]
        public string MA_NGHIEP_VU { get; set; }

        [Key]
        public Guid KEY_CHUNG_TU { get; set; }

        [StringLength(31)]
        public string MA_CHUNG_TU { get; set; }

        [StringLength(255)]
        public string TEN_CHUNG_TU { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BAN_HANG> BAN_HANG { get; set; }

        public virtual NGHIEP_VU NGHIEP_VU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHAP_XUAT> NHAP_XUAT { get; set; }
    }
}
