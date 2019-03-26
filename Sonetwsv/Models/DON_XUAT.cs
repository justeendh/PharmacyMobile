namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class DON_XUAT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DON_XUAT()
        {
            NOI_DUNG = new HashSet<NOI_DUNG>();
        }

        public Guid? KEY_CHI_NHANH { get; set; }

        [Key]
        public Guid KEY_DON_XUAT { get; set; }

        public DateTime? DAY_DON_XUAT { get; set; }

        [StringLength(31)]
        public string MAU_DON_XUAT { get; set; }

        [StringLength(31)]
        public string TAP_DON_XUAT { get; set; }

        [StringLength(31)]
        public string SER_DON_XUAT { get; set; }

        [StringLength(31)]
        public string COD_DON_XUAT { get; set; }

        [StringLength(31)]
        public string TAX_DON_XUAT { get; set; }

        [StringLength(255)]
        public string MUA_DON_XUAT { get; set; }

        [StringLength(255)]
        public string TEN_DON_XUAT { get; set; }

        [StringLength(255)]
        public string ADD_DON_XUAT { get; set; }

        [StringLength(255)]
        public string ACC_DON_XUAT { get; set; }

        [StringLength(255)]
        public string BAN_DON_XUAT { get; set; }

        [StringLength(255)]
        public string PAY_DON_XUAT { get; set; }

        [StringLength(255)]
        public string LTS_DON_XUAT { get; set; }

        public short? HSO_DON_XUAT { get; set; }

        [StringLength(255)]
        public string NOI_DON_XUAT { get; set; }

        public bool? APR_DON_XUAT { get; set; }

        public bool? PRT_DON_XUAT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? AMT_DON_XUAT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VAT_DON_XUAT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SUM_DON_XUAT { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOI_DUNG> NOI_DUNG { get; set; }
    }
}
