namespace bookworm.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class product_beneficiary_details
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product_beneficiary_details()
        {
            product_ben = new HashSet<product_ben>();
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product_ben> product_ben { get; set; }
    }
}
