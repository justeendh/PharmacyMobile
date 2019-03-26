namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class NGUOI_BAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NGUOI_BAN()
        {
            BAN_HANG = new HashSet<BAN_HANG>();
            NHAP_XUAT = new HashSet<NHAP_XUAT>();
        }

        [Key]
        public Guid KEY_NGUOI_BAN { get; set; }

        [StringLength(31)]
        public string MA_NGUOI_BAN { get; set; }

        [StringLength(255)]
        public string TEN_NGUOI_BAN { get; set; }

        public decimal? HSO_NGUOI_BAN { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BAN_HANG> BAN_HANG { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHAP_XUAT> NHAP_XUAT { get; set; }
    }
}
