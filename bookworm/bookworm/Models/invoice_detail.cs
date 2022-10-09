namespace bookworm.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class invoice_detail
    {
        [Key]
        public int InvDtl_Id { get; set; }

        public int? user_id { get; set; }

        public int? Invoice_Id { get; set; }

        public int? Product_Id { get; set; }

        public int? Quantity { get; set; }

        public double? Base_Price { get; set; }

        public double? Sale_Price { get; set; }

        public int? Offer_Price { get; set; }

        public int? net_pay { get; set; }

        public double? discount { get; set; }

        [StringLength(1)]
        public string Tran_Type { get; set; }

        public int? Rent_No_Of_Days { get; set; }

        public virtual invoice invoice { get; set; }

        public virtual product product { get; set; }

        public virtual user_master user_master { get; set; }
    }
}
