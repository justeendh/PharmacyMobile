namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class UPD_BAN_BILL
    {
        public Guid? KEY_BAN_HANG { get; set; }

        public short? COD_ROW_XLIN { get; set; }

        [Key]
        public Guid KEY_BAN_BILL { get; set; }

        public Guid? KEY_KHO_HANG { get; set; }

        public Guid? KEY_MAT_HANG { get; set; }

        [StringLength(31)]
        public string SOLO_SAN_XUAT { get; set; }

        [StringLength(31)]
        public string THOI_HAN_DUNG { get; set; }

        public DateTime? NGAY_DEN_HAN { get; set; }

        public Guid? KEY_CHI_MUC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SO_LUONG_BAN { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DON_GIA_HANG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TIEN_VIET_BAN { get; set; }

        [StringLength(31)]
        public string COD_TANG_GIAM { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TILE_GIAM_GIA { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TIEN_GIAM_GIA { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TIEN_VIET_NAM { get; set; }

        public bool? HANG_KE_DON { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GIA_VON_HANG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TILE_TICH_LUY { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TIEN_TICH_LUY { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }
    }
}
