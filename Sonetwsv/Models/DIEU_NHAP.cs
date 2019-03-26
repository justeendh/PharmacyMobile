namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class DIEU_NHAP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DIEU_NHAP()
        {
            DIEU_HANG = new HashSet<DIEU_HANG>();
        }

        public Guid? KEY_CHI_NHANH { get; set; }

        public Guid? KEY_NGUOI_DUNG { get; set; }

        [Key]
        public Guid KEY_DIEU_NHAP { get; set; }

        [StringLength(31)]
        public string SIG_DIEU_NHAP { get; set; }

        [StringLength(31)]
        public string COD_DIEU_NHAP { get; set; }

        public DateTime? DAY_DIEU_NHAP { get; set; }

        [StringLength(255)]
        public string GHI_DIEU_NHAP { get; set; }

        public bool? OKE_DIEU_NHAP { get; set; }

        public bool? OUT_DIEU_NHAP { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }

        public virtual CHI_NHANH CHI_NHANH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIEU_HANG> DIEU_HANG { get; set; }

        public virtual NGUOI_DUNG NGUOI_DUNG { get; set; }
    }
}
