namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class TIEN_GIAO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TIEN_GIAO()
        {
            TIEN_NOTE = new HashSet<TIEN_NOTE>();
        }

        public Guid? KEY_CHI_NHANH { get; set; }

        public Guid? KEY_NGUOI_DUNG { get; set; }

        public Guid? KEY_THU_NGAN { get; set; }

        [Key]
        public Guid KEY_TIEN_GIAO { get; set; }

        [StringLength(31)]
        public string SIG_TIEN_GIAO { get; set; }

        [StringLength(31)]
        public string COD_TIEN_GIAO { get; set; }

        public DateTime? DAY_TIEN_GIAO { get; set; }

        public DateTime? GIO_TIEN_GIAO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BAN_TIEN_GIAO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? THU_TIEN_GIAO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MAT_TIEN_GIAO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? THE_TIEN_GIAO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CHI_TIEN_GIAO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NOP_TIEN_GIAO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CON_TIEN_GIAO { get; set; }

        [StringLength(255)]
        public string GHI_TIEN_GIAO { get; set; }

        public bool? OKE_TIEN_GIAO { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? KET_TIEN_GIAO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? KAC_TIEN_GIAO { get; set; }

        public virtual CHI_NHANH CHI_NHANH { get; set; }

        public virtual NGUOI_DUNG NGUOI_DUNG { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TIEN_NOTE> TIEN_NOTE { get; set; }
    }
}
