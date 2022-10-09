namespace bookworm.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("product")]
    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            carts = new HashSet<cart>();
            invoice_detail = new HashSet<invoice_detail>();
            myshelves = new HashSet<myshelf>();
            product_ben = new HashSet<product_ben>();
        }

        [Key]
        public int product_id { get; set; }

        [StringLength(50)]
        public string product_name { get; set; }

        [StringLength(100)]
        public string product_english_name { get; set; }

        public int? product_type { get; set; }

        [StringLength(50)]
        public string product_image { get; set; }

        [StringLength(50)]
        public string product_pdf { get; set; }

        public double? product_baseprice { get; set; }

        public int? product_baseprice_perday { get; set; }

        public double? product_sp_cost { get; set; }

        public double? product_offerprice { get; set; }

        [Column(TypeName = "date")]
        public DateTime? product_offerprice_expirydate { get; set; }

        [StringLength(50)]
        public string product_desc_short { get; set; }

        [StringLength(500)]
        public string product_desc_long { get; set; }

        [StringLength(50)]
        public string product_isbn { get; set; }

        public int? product_author_id { get; set; }

        public int? product_publisher { get; set; }

        public int? product_language { get; set; }

        public int? product_genere { get; set; }

        public bool? is_rentable { get; set; }

        public bool? is_library { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cart> carts { get; set; }

        public virtual genere genere { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<invoice_detail> invoice_detail { get; set; }

        public virtual language language { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<myshelf> myshelves { get; set; }

        public virtual product_type product_type1 { get; set; }

        public virtual publisher publisher { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product_ben> product_ben { get; set; }
    }
}
