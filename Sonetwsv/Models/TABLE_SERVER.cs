namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class TABLE_SERVER
    {
        [Key]
        [StringLength(31)]
        public string TABLE_NAME { get; set; }

        [StringLength(255)]
        public string TABLE_NOTE { get; set; }

        public short? TABLE_ORDE { get; set; }

        [StringLength(31)]
        public string TABLE_STAT { get; set; }

        [StringLength(31)]
        public string TABLE_CODE { get; set; }
    }
}
