namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class UPD_BGCHI_TIET
    {
        public Guid? KEY_BANG_PRICE { get; set; }

        public short? COD_ROW_XLIN { get; set; }

        [Key]
        public Guid KEY_BGCHI_TIET { get; set; }

        public Guid? KEY_MAT_HANG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SO_LUONG_BAO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DON_GIA_BAO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SO_TIEN_BAO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? HE_SO_THUE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SO_TIEN_THUE { get; set; }

        [StringLength(255)]
        public string GHI_GHI_CHU { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }
    }
}
