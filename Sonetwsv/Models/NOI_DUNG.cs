namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class NOI_DUNG
    {
        public Guid? KEY_DON_XUAT { get; set; }

        [Key]
        public Guid KEY_NOI_DUNG { get; set; }

        public short? IDX_NOI_DUNG { get; set; }

        [StringLength(31)]
        public string STT_NOI_DUNG { get; set; }

        [StringLength(255)]
        public string TEN_NOI_DUNG { get; set; }

        [StringLength(31)]
        public string DVT_NOI_DUNG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? QTY_NOI_DUNG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GIA_NOI_DUNG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? AMT_NOI_DUNG { get; set; }

        [StringLength(255)]
        public string NOT_NOI_DUNG { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }

        public virtual DON_XUAT DON_XUAT { get; set; }
    }
}
