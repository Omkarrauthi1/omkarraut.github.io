namespace bookworm.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cart")]
    public partial class cart
    {
        [Key]
        public int Cart_id { get; set; }

        public int? Product_id { get; set; }

        public int? user_id { get; set; }

        [StringLength(1)]
        public string is_selected { get; set; }

        public virtual product product { get; set; }

        public virtual user_master user_master { get; set; }
    }
}
