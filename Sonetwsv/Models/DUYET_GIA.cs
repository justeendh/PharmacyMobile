namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class DUYET_GIA
    {
        [Key]
        public Guid KEY_DUYET_GIA { get; set; }

        public Guid? KEY_CHI_NHANH { get; set; }

        public Guid? KEY_MAT_HANG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GIA_BAN_LE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GIA_BAN_SI { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }
    }
}
