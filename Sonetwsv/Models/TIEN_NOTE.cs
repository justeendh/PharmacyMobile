namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class TIEN_NOTE
    {
        public Guid? KEY_TIEN_GIAO { get; set; }

        [Key]
        public Guid KEY_TIEN_NOTE { get; set; }

        public short? IDX_TIEN_NOTE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? AMT_TIEN_NOTE { get; set; }

        [StringLength(255)]
        public string GHI_TIEN_NOTE { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }

        public virtual TIEN_GIAO TIEN_GIAO { get; set; }
    }
}
