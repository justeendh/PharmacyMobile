namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class TABLE_FLAGS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CODE_TABLE_FLAGS { get; set; }

        public int? SERVER_SEND_DATA { get; set; }

        public int? CLIENT_RECV_DATA { get; set; }

        public int? CLIENT_MOVE_SEND { get; set; }

        public int? CLIENT_MOVE_RECV { get; set; }

        public int? CLIENT_MOVE_HOAN { get; set; }
    }
}
