namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class ORDER_GOODS : BaseModel
    {
        public Guid? KEY_ORDER_CLIENT { get; set; }
        
        public Guid KEY_ORDER_GOODS { get; set; }

        public short? IDX_ORDER_GOODS { get; set; }

        public Guid? KEY_MAT_HANG { get; set; }

        public decimal? SO_LUONG_HANG { get; set; }

        public decimal? DON_GIA_HANG { get; set; }

        public decimal? TIEN_VIET_NAM { get; set; }

    }
}
