namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class ORDER_CLIENT : BaseModel
    {
        public ORDER_CLIENT()
        {

        }

        [Key]
        public Guid KEY_ORDER_CLIENT { get; set; }

        public Guid? KEY_CARD_CLIENT { get; set; }

        public DateTime? DAY_ORDER_CLIENT { get; set; }

        [StringLength(31)]
        public string COD_ORDER_CLIENT { get; set; }

        public decimal? TONG_TIEN_HANG { get; set; }

        public decimal? TONG_GIAM_GIA { get; set; }

        public decimal? TONG_TIEN_TOAN { get; set; }

        public bool? STA_ORDER_CLIENT { get; set; }

    }
}
