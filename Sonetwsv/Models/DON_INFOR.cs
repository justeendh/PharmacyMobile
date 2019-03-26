namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class DON_INFOR
    {
        [Key]
        public Guid KEY_DON_INFOR { get; set; }

        [StringLength(31)]
        public string TAX_DON_INFOR { get; set; }

        [StringLength(255)]
        public string TEN_DON_INFOR { get; set; }

        [StringLength(255)]
        public string ORG_DON_INFOR { get; set; }

        [StringLength(255)]
        public string ADD_DON_INFOR { get; set; }

        [StringLength(255)]
        public string PAY_DON_INFOR { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }
    }
}
