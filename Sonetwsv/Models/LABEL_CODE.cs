namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class LABEL_CODE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short MA_THAM_SO { get; set; }

        public short? MAX_LABEL_CODE { get; set; }

        public short? COD_LABEL_CODE { get; set; }

        public short? NUM_LABEL_CODE { get; set; }

        [StringLength(31)]
        public string TEN_LABEL_CODE { get; set; }

        public short? NUM_CODE_LOAI { get; set; }

        public short? NUM_CODE_TYPE { get; set; }

        public short? NUM_CODE_NHOM { get; set; }

        public short? NUM_CODE_HANG { get; set; }

        public short? NUM_CODE_NUOC { get; set; }
    }
}
