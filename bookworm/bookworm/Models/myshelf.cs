namespace bookworm.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("myshelf")]
    public partial class myshelf
    {
        [Key]
        public int Shelf_id { get; set; }

        public int? Customer_id { get; set; }

        public int? Product_id { get; set; }

        [StringLength(1)]
        public string tran_type { get; set; }

        [Column(TypeName = "date")]
        public DateTime? product_ExpiryDate { get; set; }

        [StringLength(1)]
        public string isActive { get; set; }

        public virtual user_master user_master { get; set; }

        public virtual product product { get; set; }
    }
}
