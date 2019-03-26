namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class THANG_DUBYT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short MA_THANG_DUBYT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GIA_MUA_DUOI01 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TILE_BAN_MUC01 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GIA_MUA_DUOI02 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GIA_MUA_TREN02 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TILE_BAN_MUC02 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GIA_MUA_DUOI03 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GIA_MUA_TREN03 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TILE_BAN_MUC03 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GIA_MUA_DUOI04 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GIA_MUA_TREN04 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TILE_BAN_MUC04 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GIA_MUA_TREN05 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TILE_BAN_MUC05 { get; set; }
    }
}
