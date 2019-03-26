namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class CALL_HANG
    {
        public Guid? KEY_CHI_NHANH { get; set; }

        [Key]
        public Guid KEY_CALL_HANG { get; set; }

        public DateTime? NGAY_CALL_HANG { get; set; }

        public Guid? KEY_MAT_HANG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SO_LUONG_HANG { get; set; }
    }
}
