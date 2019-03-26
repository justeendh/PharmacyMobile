namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class THAM_SO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short MA_THAM_SO { get; set; }

        public short? KY_KE_TOAN { get; set; }

        public short? NAM_KE_TOAN { get; set; }

        public short? KY_HIEN_TAI { get; set; }

        public short? NAM_HIEN_TAI { get; set; }

        public short? KY_BAO_CAO { get; set; }

        public short? DON_GIA_KHO { get; set; }

        [StringLength(255)]
        public string NGUOI_LAP_BIEU { get; set; }

        [StringLength(255)]
        public string KE_TOAN_TRUONG { get; set; }

        [StringLength(255)]
        public string TEN_GIAM_DOC { get; set; }

        [StringLength(255)]
        public string COMPANY_NAME { get; set; }

        [StringLength(255)]
        public string COMPANY_ADDR { get; set; }

        [StringLength(31)]
        public string COMPANY_CODE { get; set; }

        [StringLength(255)]
        public string PATH_LOGO_PIC { get; set; }

        public short? AP_TIEU_CHUAN { get; set; }

        public short? VOUCH_TO_BILL { get; set; }

        public short? KIEU_TRUY_CAP { get; set; }

        public short? KIEM_HANG_TON { get; set; }

        public short? KIEM_NHAP_KHO { get; set; }

        public bool? LOCK_DAY_BILL { get; set; }

        public DateTime? DATE_DAY_BILL { get; set; }

        public short? RUN_LOAI_SHOP { get; set; }

        public short? IN_TEM_BARCOD { get; set; }

        public short? KIEU_SYN_DATA { get; set; }
    }
}
