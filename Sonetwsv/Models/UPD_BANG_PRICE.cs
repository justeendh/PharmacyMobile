namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class UPD_BANG_PRICE
    {
        public Guid? KEY_CHI_NHANH { get; set; }

        [Key]
        public Guid KEY_BANG_PRICE { get; set; }

        [StringLength(31)]
        public string SO_BANG_PRICE { get; set; }

        public DateTime? NGAY_BANG_PRICE { get; set; }

        [StringLength(255)]
        public string TIEU_BANG_PRICE { get; set; }

        [StringLength(255)]
        public string DON_VI_NHAN { get; set; }

        [StringLength(255)]
        public string DIA_CHI_NHAN { get; set; }

        [StringLength(255)]
        public string TEN_DAI_DIEN { get; set; }

        [StringLength(255)]
        public string SO_DIEN_THOAI { get; set; }

        [StringLength(255)]
        public string SO_FAX_KHACH { get; set; }

        [StringLength(255)]
        public string THOI_HAN_GIAO { get; set; }

        public DateTime? NGAY_HIEU_LUC { get; set; }

        [StringLength(255)]
        public string KIEU_TRA_TIEN { get; set; }

        [StringLength(255)]
        public string DIA_DIEM_GIAO { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }
    }
}
