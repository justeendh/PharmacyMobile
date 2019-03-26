namespace Sonetwsv
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class BANG_VON
    {
        public Guid? KEY_NHAP_XUAT { get; set; }

        [Key]
        public Guid KEY_BANG_VON { get; set; }

        public Guid? KEY_KHACH_HANG { get; set; }

        public Guid? KEY_GIAO_DICH { get; set; }

        public Guid? KEY_THANH_TOAN { get; set; }

        public Guid? KEY_KHOAN_MUC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TIEN_VIET_NAM { get; set; }

        [StringLength(255)]
        public string GHI_DIEN_GIAI { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }

        public Guid? KEY_MUA_BAN { get; set; }

        public virtual GIAO_DICH GIAO_DICH { get; set; }

        public virtual GIAO_DICH GIAO_DICH1 { get; set; }

        public virtual KHOAN_MUC KHOAN_MUC { get; set; }

        public virtual NHAP_XUAT NHAP_XUAT { get; set; }
    }
}
