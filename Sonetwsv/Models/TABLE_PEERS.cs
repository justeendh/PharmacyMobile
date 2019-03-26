namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class TABLE_PEERS
    {
        [Key]
        public Guid KEY_TABLE_PEERS { get; set; }

        [StringLength(31)]
        public string SED_TABLE_PEERS { get; set; }

        [StringLength(31)]
        public string REV_TABLE_PEERS { get; set; }

        public int? NUM_TABLE_PEERS { get; set; }
    }
}
