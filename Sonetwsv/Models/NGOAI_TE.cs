namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class NGOAI_TE
    {
        [Key]
        public Guid KEY_NGOAI_TE { get; set; }

        [StringLength(31)]
        public string MA_NGOAI_TE { get; set; }

        [StringLength(255)]
        public string TEN_NGOAI_TE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GIA_HIEN_TAI { get; set; }

        public bool? CO_MAC_DINH { get; set; }
    }
}
