namespace bookworm.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class user_master
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user_master()
        {
            carts = new HashSet<cart>();
            invoice_detail = new HashSet<invoice_detail>();
            myshelves = new HashSet<myshelf>();
        }

        [Key]
        public int user_id { get; set; }

        [StringLength(30)]
        public string first_name { get; set; }

        [StringLength(30)]
        public string last_name { get; set; }

        [StringLength(25)]
        public string user_name { get; set; }

        [StringLength(25)]
        public string email_id { get; set; }

        [StringLength(20)]
        public string password { get; set; }

        [StringLength(10)]
        public string mobile_no { get; set; }

        public int? role_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cart> carts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<invoice_detail> invoice_detail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<myshelf> myshelves { get; set; }

        public virtual role role { get; set; }
    }
}
