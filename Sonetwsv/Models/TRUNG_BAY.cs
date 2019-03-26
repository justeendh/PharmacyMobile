namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class TRUNG_BAY
    {
        [Key]
        public Guid KEY_TRUNG_BAY { get; set; }

        public Guid? KEY_MAT_HANG { get; set; }

        [StringLength(255)]
        public string KTB_MAT_HANG { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }
    }
}
