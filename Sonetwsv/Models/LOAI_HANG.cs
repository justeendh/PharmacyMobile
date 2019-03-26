namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class LOAI_HANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOAI_HANG()
        {
            MAT_HANG = new HashSet<MAT_HANG>();
        }

        [Key]
        public Guid KEY_LOAI_HANG { get; set; }

        [StringLength(31)]
        public string MA_LOAI_HANG { get; set; }

        [StringLength(255)]
        public string TEN_LOAI_HANG { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MAT_HANG> MAT_HANG { get; set; }
    }
}
