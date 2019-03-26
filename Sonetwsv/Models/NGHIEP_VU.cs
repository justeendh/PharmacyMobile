namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class NGHIEP_VU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NGHIEP_VU()
        {
            CHUNG_TU = new HashSet<CHUNG_TU>();
        }

        [Key]
        [StringLength(31)]
        public string MA_NGHIEP_VU { get; set; }

        [StringLength(255)]
        public string TEN_NGHIEP_VU { get; set; }

        [StringLength(31)]
        public string MA_LOAI_VIEW { get; set; }

        public short? MA_LOAI_SORT { get; set; }

        [StringLength(31)]
        public string MA_LOAI_XULY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHUNG_TU> CHUNG_TU { get; set; }
    }
}
