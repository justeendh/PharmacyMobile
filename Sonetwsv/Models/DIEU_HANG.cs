namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class DIEU_HANG
    {
        public Guid? KEY_DIEU_NHAP { get; set; }

        public short? COD_ROW_XLIN { get; set; }

        [Key]
        public Guid KEY_DIEU_HANG { get; set; }

        public Guid? KEY_MAT_HANG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SO_LUONG_HANG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DON_GIA_HANG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TIEN_VIET_HANG { get; set; }

        [StringLength(255)]
        public string GHI_CHU_HANG { get; set; }

        public short? NOI_DIEU_HANG { get; set; }

        public bool? GOI_DIEU_HANG { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }

        public virtual DIEU_NHAP DIEU_NHAP { get; set; }

        public virtual MAT_HANG MAT_HANG { get; set; }
    }
}
