namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class SODU_HANG
    {
        public Guid? KEY_KHO_HANG { get; set; }

        public Guid? KEY_MAT_HANG { get; set; }

        [Key]
        public Guid KEY_SODU_HANG { get; set; }

        public short? KY_KE_TOAN { get; set; }

        public short? NAM_KE_TOAN { get; set; }

        [StringLength(31)]
        public string SOLO_SAN_XUAT { get; set; }

        [StringLength(31)]
        public string THOI_HAN_DUNG { get; set; }

        public DateTime? NGAY_DEN_HAN { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SO_LUONG_HANG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DON_GIA_HANG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TIEN_VIET_NAM { get; set; }

        public DateTime? NGAY_DAU_KY { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }

        public virtual KHO_HANG KHO_HANG { get; set; }

        public virtual MAT_HANG MAT_HANG { get; set; }
    }
}
