namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class GIAO_DICH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GIAO_DICH()
        {
            BANG_VON = new HashSet<BANG_VON>();
            BANG_VON1 = new HashSet<BANG_VON>();
            DUNO_KHACH = new HashSet<DUNO_KHACH>();
            SODU_KHACH = new HashSet<SODU_KHACH>();
        }

        [Key]
        public Guid KEY_GIAO_DICH { get; set; }

        [StringLength(31)]
        public string COD_GIAO_DICH { get; set; }

        [StringLength(255)]
        public string TEN_GIAO_DICH { get; set; }

        [StringLength(31)]
        public string COD_LOAI_DICH { get; set; }

        [StringLength(31)]
        public string LOAI_CONG_NO { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BANG_VON> BANG_VON { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BANG_VON> BANG_VON1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DUNO_KHACH> DUNO_KHACH { get; set; }

        public virtual LOAI_DICH LOAI_DICH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SODU_KHACH> SODU_KHACH { get; set; }
    }
}
