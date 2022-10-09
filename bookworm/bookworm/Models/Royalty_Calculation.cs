namespace bookworm.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Royalty_Calculation
    {
        [Key]
        public int roycal_id { get; set; }

        public int? Invoice_Id { get; set; }

        public int? Ben_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? roycal_tran_date { get; set; }

        public int? Product_Id { get; set; }

        public int? roycal_qty { get; set; }

        [StringLength(1)]
        public string trantype { get; set; }

        public double? baseprice { get; set; }

        public double? saleprice { get; set; }

        public double? RoyaltyOnBasePrice { get; set; }
    }
}
