namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class LOAI_DICH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOAI_DICH()
        {
            GIAO_DICH = new HashSet<GIAO_DICH>();
        }

        [Key]
        [StringLength(31)]
        public string COD_LOAI_DICH { get; set; }

        [StringLength(255)]
        public string TEN_LOAI_DICH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GIAO_DICH> GIAO_DICH { get; set; }
    }
}
