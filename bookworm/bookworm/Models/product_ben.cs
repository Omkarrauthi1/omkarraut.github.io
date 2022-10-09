namespace bookworm.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class product_ben
    {
        [Key]
        public int ProdBen_id { get; set; }

        public int? ProdBen_ben_id { get; set; }

        public int? ProdBen_product_id { get; set; }

        public double? ProdBen_percentage { get; set; }

        public virtual product product { get; set; }

        public virtual product_beneficiary_details product_beneficiary_details { get; set; }
    }
}
