namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class UPD_KHIEU_NAI
    {
        public Guid? KEY_CHI_NHANH { get; set; }

        [Key]
        public Guid KEY_KHIEU_NAI { get; set; }

        public DateTime? NGAY_GHI_NHAN { get; set; }

        [StringLength(31)]
        public string SO_GHI_PHIEU { get; set; }

        public Guid? KEY_KHACH_HANG { get; set; }

        [StringLength(255)]
        public string TEN_KHACH_HANG { get; set; }

        [StringLength(255)]
        public string DIA_CHI_KHACH { get; set; }

        [StringLength(255)]
        public string NGUOI_KE_TOA { get; set; }

        [StringLength(255)]
        public string NOI_DIEU_TRI { get; set; }

        public Guid? KEY_NGUOI_DUNG { get; set; }

        [StringLength(1023)]
        public string NOI_DUNG_GHI { get; set; }

        [StringLength(1023)]
        public string GHI_KET_LUAN { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }
    }
}
