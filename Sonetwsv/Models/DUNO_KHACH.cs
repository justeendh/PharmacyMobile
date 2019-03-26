namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class DUNO_KHACH
    {
        public Guid? KEY_CHI_NHANH { get; set; }

        [Key]
        public Guid KEY_DUNO_KHACH { get; set; }

        public Guid? KEY_KHACH_HANG { get; set; }

        public Guid? KEY_GIAO_DICH { get; set; }

        public short? KY_KE_TOAN { get; set; }

        public short? NAM_KE_TOAN { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TIEN_MUA_BAN { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TIEN_NODA_TRA { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }

        public virtual GIAO_DICH GIAO_DICH { get; set; }

        public virtual KHACH_HANG KHACH_HANG { get; set; }
    }
}
