namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class CHINH_GIA
    {
        [Key]
        public Guid KEY_CHINH_GIA { get; set; }

        public Guid? KEY_MAT_HANG { get; set; }

        public Guid? KEY_NGUOI_DUNG { get; set; }

        public DateTime? DAY_CHINH_GIA { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GIA_BAN_LE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GIA_BAN_SI { get; set; }

        public virtual MAT_HANG MAT_HANG { get; set; }
    }
}
