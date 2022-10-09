using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace bookworm.Models
{
    public partial class BookwormModel : DbContext
    {
        public BookwormModel()
            : base("name=BookwormModelConnection")
        {
            //this.Configuration.LazyLoadingEnabled = false;

        }

        public virtual DbSet<cart> carts { get; set; }
        public virtual DbSet<genere> generes { get; set; }
        public virtual DbSet<invoice> invoices { get; set; }
        public virtual DbSet<invoice_detail> invoice_detail { get; set; }
        public virtual DbSet<language> languages { get; set; }
        public virtual DbSet<myshelf> myshelves { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<product_ben> product_ben { get; set; }
        public virtual DbSet<product_beneficiary_details> product_beneficiary_details { get; set; }
        public virtual DbSet<product_type> product_type { get; set; }
        public virtual DbSet<publisher> publishers { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<Royalty_Calculation> Royalty_Calculation { get; set; }
        public virtual DbSet<user_master> user_master { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cart>()
                .Property(e => e.is_selected)
                .IsUnicode(false);

            modelBuilder.Entity<genere>()
                .Property(e => e.genere_desc)
                .IsUnicode(false);

            modelBuilder.Entity<genere>()
                .HasMany(e => e.products)
                .WithOptional(e => e.genere)
                .HasForeignKey(e => e.product_genere);

            modelBuilder.Entity<invoice>()
                .Property(e => e.tran_type)
                .IsUnicode(false);

            modelBuilder.Entity<invoice_detail>()
                .Property(e => e.Tran_Type)
                .IsUnicode(false);

            modelBuilder.Entity<language>()
                .Property(e => e.lang_desc)
                .IsUnicode(false);

            modelBuilder.Entity<language>()
                .HasMany(e => e.products)
                .WithOptional(e => e.language)
                .HasForeignKey(e => e.product_language);

            modelBuilder.Entity<myshelf>()
                .Property(e => e.tran_type)
                .IsUnicode(false);

            modelBuilder.Entity<myshelf>()
                .Property(e => e.isActive)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.product_name)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.product_english_name)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.product_image)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.product_pdf)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.product_desc_short)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.product_desc_long)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.product_isbn)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.product_ben)
                .WithOptional(e => e.product)
                .HasForeignKey(e => e.ProdBen_product_id);

            modelBuilder.Entity<product_beneficiary_details>()
                .Property(e => e.Ben_name)
                .IsUnicode(false);

            modelBuilder.Entity<product_beneficiary_details>()
                .Property(e => e.Ben_email_id)
                .IsUnicode(false);

            modelBuilder.Entity<product_beneficiary_details>()
                .Property(e => e.Ben_Contact_no)
                .IsUnicode(false);

            modelBuilder.Entity<product_beneficiary_details>()
                .Property(e => e.Ben_bank_name)
                .IsUnicode(false);

            modelBuilder.Entity<product_beneficiary_details>()
                .Property(e => e.Ben_bank_Branch)
                .IsUnicode(false);

            modelBuilder.Entity<product_beneficiary_details>()
                .Property(e => e.Ben_IFSC)
                .IsUnicode(false);

            modelBuilder.Entity<product_beneficiary_details>()
                .Property(e => e.Ben_AccNo)
                .IsUnicode(false);

            modelBuilder.Entity<product_beneficiary_details>()
                .Property(e => e.Ben_Acc_Type)
                .IsUnicode(false);

            modelBuilder.Entity<product_beneficiary_details>()
                .Property(e => e.Ben_PAN)
                .IsUnicode(false);

            modelBuilder.Entity<product_beneficiary_details>()
                .HasMany(e => e.product_ben)
                .WithOptional(e => e.product_beneficiary_details)
                .HasForeignKey(e => e.ProdBen_ben_id);

            modelBuilder.Entity<product_type>()
                .Property(e => e.type_desc)
                .IsUnicode(false);

            modelBuilder.Entity<product_type>()
                .HasMany(e => e.products)
                .WithOptional(e => e.product_type1)
                .HasForeignKey(e => e.product_type);

            modelBuilder.Entity<publisher>()
                .Property(e => e.Ben_name)
                .IsUnicode(false);

            modelBuilder.Entity<publisher>()
                .Property(e => e.Ben_email_id)
                .IsUnicode(false);

            modelBuilder.Entity<publisher>()
                .Property(e => e.Ben_Contact_no)
                .IsUnicode(false);

            modelBuilder.Entity<publisher>()
                .Property(e => e.Ben_bank_name)
                .IsUnicode(false);

            modelBuilder.Entity<publisher>()
                .Property(e => e.Ben_bank_Branch)
                .IsUnicode(false);

            modelBuilder.Entity<publisher>()
                .Property(e => e.Ben_IFSC)
                .IsUnicode(false);

            modelBuilder.Entity<publisher>()
                .Property(e => e.Ben_AccNo)
                .IsUnicode(false);

            modelBuilder.Entity<publisher>()
                .Property(e => e.Ben_Acc_Type)
                .IsUnicode(false);

            modelBuilder.Entity<publisher>()
                .Property(e => e.Ben_PAN)
                .IsUnicode(false);

            modelBuilder.Entity<publisher>()
                .Property(e => e.Ben_user_name)
                .IsUnicode(false);

            modelBuilder.Entity<publisher>()
                .Property(e => e.Ben_password)
                .IsUnicode(false);

            modelBuilder.Entity<publisher>()
                .HasMany(e => e.products)
                .WithOptional(e => e.publisher)
                .HasForeignKey(e => e.product_publisher);

            modelBuilder.Entity<role>()
                .Property(e => e.role_name)
                .IsUnicode(false);

            modelBuilder.Entity<role>()
                .HasMany(e => e.user_master)
                .WithOptional(e => e.role)
                .HasForeignKey(e => e.role_id);

            modelBuilder.Entity<Royalty_Calculation>()
                .Property(e => e.trantype)
                .IsUnicode(false);

            modelBuilder.Entity<user_master>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<user_master>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<user_master>()
                .Property(e => e.user_name)
                .IsUnicode(false);

            modelBuilder.Entity<user_master>()
                .Property(e => e.email_id)
                .IsUnicode(false);

            modelBuilder.Entity<user_master>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user_master>()
                .Property(e => e.mobile_no)
                .IsUnicode(false);

            modelBuilder.Entity<user_master>()
                .HasMany(e => e.myshelves)
                .WithOptional(e => e.user_master)
                .HasForeignKey(e => e.Customer_id);
        }
    }
}
