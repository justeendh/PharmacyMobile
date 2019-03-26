namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class POS_CONFIG
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short MA_THAM_SO { get; set; }

        public short? AP_TIEU_CHUAN { get; set; }

        public short? MAU_MAY_PRINT { get; set; }

        [StringLength(255)]
        public string TEN_NHAN_HIEU { get; set; }

        [StringLength(255)]
        public string CAU_KHAU_HIEU { get; set; }

        [StringLength(255)]
        public string TIEU_DONG_MOT { get; set; }

        [StringLength(255)]
        public string TIEU_DONG_HAI { get; set; }

        [StringLength(255)]
        public string TIEU_DONG_TAM { get; set; }

        [StringLength(255)]
        public string PATH_LOGO_PIC { get; set; }

        public short? EDIT_SO_LUONG { get; set; }

        public short? CHUA_HANG_GUI { get; set; }

        public Guid? KHO_HANG_GUI { get; set; }

        public Guid? KEY_KHO_HANG { get; set; }

        public Guid? KEY_CHI_NHANH { get; set; }

        public Guid? KEY_CHUNG_TU { get; set; }

        [StringLength(31)]
        public string COD_QUAY_BAN { get; set; }

        public short? TANG_GIAM_GIA { get; set; }

        public short? KIEU_TIM_HANG { get; set; }

        [StringLength(255)]
        public string TEN_PRINT_BILL { get; set; }

        [StringLength(255)]
        public string DVF_PORTS_NAME { get; set; }

        public short? DVF_BAUDS_RATE { get; set; }

        [StringLength(255)]
        public string DVF_TOTAL_TIEN { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DIEM_QUI_UOC { get; set; }

        public short? DIEM_TRU_DOI { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MUC_TANG_QUA { get; set; }

        public short? KIEU_TRUY_CAP { get; set; }

        public short? MUC_DIEM_DOI { get; set; }

        public DateTime? DAY_HET_TICH { get; set; }

        public short? RUN_TANG_QUA { get; set; }

        public short? MUC_TRUC_TIEP { get; set; }
    }
}
