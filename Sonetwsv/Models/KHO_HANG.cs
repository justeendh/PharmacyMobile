namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class KHO_HANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHO_HANG()
        {
            BAN_BILL = new HashSet<BAN_BILL>();
            PUTIN_OUT = new HashSet<PUTIN_OUT>();
            SODU_HANG = new HashSet<SODU_HANG>();
        }

        [Key]
        public Guid KEY_KHO_HANG { get; set; }

        [StringLength(31)]
        public string MA_KHO_HANG { get; set; }

        [StringLength(255)]
        public string TEN_KHO_HANG { get; set; }

        [StringLength(255)]
        public string DIA_CHI_KHO { get; set; }

        [StringLength(255)]
        public string SUC_CHUA_KHO { get; set; }

        [StringLength(255)]
        public string TEN_THU_KHO { get; set; }

        public bool? HANG_KY_GUI { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }

        public short? IDX_KHO_HANG { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BAN_BILL> BAN_BILL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PUTIN_OUT> PUTIN_OUT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SODU_HANG> SODU_HANG { get; set; }
    }
}
