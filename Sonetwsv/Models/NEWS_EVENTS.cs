namespace Sonetwsv
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class NEWS_EVENTS
    {
        [Key]
        public Guid KEY_NEWS_EVENTS { get; set; }

        public DateTime? DAY_NEWS_EVENTS { get; set; }

        [StringLength(255)]
        public string TEN_NEWS_EVENTS { get; set; }

        [StringLength(255)]
        public string ANH_NEWS_EVENTS { get; set; }

        public string NOI_NEWS_EVENTS { get; set; }

        public bool? ACT_NEWS_EVENTS { get; set; }
    }
}
