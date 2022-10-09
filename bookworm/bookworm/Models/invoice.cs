namespace bookworm.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("invoice")]
    public partial class invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public invoice()
        {
            invoice_detail = new HashSet<invoice_detail>();
        }

        [Key]
        public int Invoice_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Invoice_date { get; set; }

        public int? cust_id { get; set; }

        public double? Invoice_amount { get; set; }

        [StringLength(1)]
        public string tran_type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<invoice_detail> invoice_detail { get; set; }
    }
}
