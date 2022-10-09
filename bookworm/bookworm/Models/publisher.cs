namespace bookworm.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("publisher")]
    public partial class publisher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public publisher()
        {
            products = new HashSet<product>();
        }

        [Key]
        public int Ben_id { get; set; }

        [StringLength(100)]
        public string Ben_name { get; set; }

        [StringLength(40)]
        public string Ben_email_id { get; set; }

        [StringLength(10)]
        public string Ben_Contact_no { get; set; }

        [StringLength(60)]
        public string Ben_bank_name { get; set; }

        [StringLength(40)]
        public string Ben_bank_Branch { get; set; }

        [StringLength(10)]
        public string Ben_IFSC { get; set; }

        [StringLength(20)]
        public string Ben_AccNo { get; set; }

        [StringLength(20)]
        public string Ben_Acc_Type { get; set; }

        [StringLength(10)]
        public string Ben_PAN { get; set; }

        [StringLength(50)]
        public string Ben_user_name { get; set; }

        [StringLength(50)]
        public string Ben_password { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product> products { get; set; }
    }
}
