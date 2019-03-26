namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class CARD_CLIENT : BaseModel
    {
        public CARD_CLIENT()
        {
            BAN_HANG = new HashSet<BAN_HANG>();
        }
        
        public Guid KEY_CARD_CLIENT { get; set; }

        [StringLength(31)]
        public string COD_CARD_CLIENT { get; set; }

        [StringLength(255)]
        public string TEN_CARD_CLIENT { get; set; }

        [StringLength(31)]
        public string SEX_CARD_CLIENT { get; set; }

        public DateTime? BIR_CARD_CLIENT { get; set; }

        [StringLength(255)]
        public string TEL_CARD_CLIENT { get; set; }

        [StringLength(255)]
        public string EML_CARD_CLIENT { get; set; }

        [StringLength(255)]
        public string ADD_CARD_CLIENT { get; set; }

        [StringLength(255)]
        public string ORG_CARD_CLIENT { get; set; }

        public int? BAN_CARD_CLIENT { get; set; }

        public int? DOI_CARD_CLIENT { get; set; }

        public int? CON_CARD_CLIENT { get; set; }

        public DateTime? DATE_DONG_BO { get; set; }

        public short? VERS_DONG_BO { get; set; }

        public bool? FLAG_DONG_BO { get; set; }
        
        public virtual ICollection<BAN_HANG> BAN_HANG { get; set; }
    }
}
