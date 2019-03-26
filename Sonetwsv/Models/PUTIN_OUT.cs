namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class PUTIN_OUT
    {
        public Guid? KEY_NHAP_XUAT { get; set; }

        public Guid? KEY_KHO_HANG { get; set; }

        public Guid? KEY_KHO_MOVE { get; set; }

        public Guid? KEY_MAT_HANG { get; set; }

        [Key]
        public Guid KEY_PUTIN_OUT { get; set; }

        public short? COD_ROW_XLIN { get; set; }

        public Guid? KEY_CHI_MUC { get; set; }

        public Guid? KEY_HANG_FIFO { get; set; }

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
        public decimal? TIEN_VIET_HANG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SO_CHIET_KHAU { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TIEN_CHIET_KHAU { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TIEN_SAU_KHAU { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? HE_SO_THUE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GIA_SAU_THUE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TIEN_VIET_THUE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TIEN_VIET_NAM { get; set; }

        [StringLength(255)]
        public string GHI_DIEN_GIAI { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }

        public virtual KHO_HANG KHO_HANG { get; set; }

        public virtual MAT_HANG MAT_HANG { get; set; }

        public virtual NHAP_XUAT NHAP_XUAT { get; set; }
    }
}
