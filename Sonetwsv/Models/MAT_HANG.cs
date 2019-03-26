namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class MAT_HANG : BaseModel
    {
        public MAT_HANG()
        {
        }

        public Guid? KEY_LOAI_HANG { get; set; }

        public Guid? KEY_NHOM_HANG { get; set; }

        public Guid KEY_MAT_HANG { get; set; }

        [StringLength(31)]
        public string MA_MAT_HANG { get; set; }

        [StringLength(255)]
        public string TEN_MAT_HANG { get; set; }

        [StringLength(31)]
        public string MA_SAN_XUAT { get; set; }

        [StringLength(31)]
        public string DON_VI_TINH { get; set; }

        [StringLength(31)]
        public string DON_VI_LON { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SO_QUI_DOI { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TILE_GIAM_GIA { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? HE_SO_THUE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TILE_TIEN_LAI { get; set; }

        public bool? HANG_KE_DON { get; set; }

        public bool? HANG_DAC_BIET { get; set; }

        public bool? HANG_KY_GUI { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DON_GIA_MUA { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GIA_BAN_LE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GIA_BAN_SI { get; set; }

        [StringLength(255)]
        public string GHI_GHI_CHU { get; set; }

        [StringLength(255)]
        public string GHI_HAM_LUONG { get; set; }

        [StringLength(255)]
        public string GHI_HOAT_CHAT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TON_TOI_THIEU { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TILE_MOI_GIOI { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TILE_GHI_DIEM { get; set; }

        public bool? CO_SU_DUNG { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }

        public bool? HANG_TAM_THAN { get; set; }

        [StringLength(31)]
        public string SO_DANG_KY { get; set; }

        [StringLength(255)]
        public string HANG_SAN_XUAT { get; set; }
        
    }
}
