namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class KHOAN_MUC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHOAN_MUC()
        {
            BANG_VON = new HashSet<BANG_VON>();
        }

        [Key]
        public Guid KEY_KHOAN_MUC { get; set; }

        [StringLength(31)]
        public string COD_KHOAN_MUC { get; set; }

        [StringLength(255)]
        public string TEN_KHOAN_MUC { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BANG_VON> BANG_VON { get; set; }
    }
}
