using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
    public class ContactMap : EntityTypeConfiguration<Contact>
    {
        public ContactMap()
        {
            // Primary Key
            this.HasKey(t => t.contactId);

            // Properties
            this.Property(t => t.IBONum)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.firstName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.lastName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.email)
                .HasMaxLength(100);

            this.Property(t => t.phone)
                .HasMaxLength(20);

            this.Property(t => t.cell)
                .HasMaxLength(20);

            this.Property(t => t.address)
                .HasMaxLength(250);

            this.Property(t => t.state)
                .HasMaxLength(50);

            this.Property(t => t.city)
                .HasMaxLength(50);

            this.Property(t => t.zipcode)
                .HasMaxLength(10);

            this.Property(t => t.preferred)
                .HasMaxLength(50);

            this.Property(t => t.contactLevel)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Contacts");
            this.Property(t => t.contactId).HasColumnName("contactId");
            this.Property(t => t.IBONum).HasColumnName("IBONum");
            this.Property(t => t.contactTypeId).HasColumnName("contactTypeId");
            this.Property(t => t.languageId).HasColumnName("languageId");
            this.Property(t => t.firstName).HasColumnName("firstName");
            this.Property(t => t.lastName).HasColumnName("lastName");
            this.Property(t => t.email).HasColumnName("email");
            this.Property(t => t.phone).HasColumnName("phone");
            this.Property(t => t.cell).HasColumnName("cell");
            this.Property(t => t.address).HasColumnName("address");
            this.Property(t => t.state).HasColumnName("state");
            this.Property(t => t.city).HasColumnName("city");
            this.Property(t => t.zipcode).HasColumnName("zipcode");
            this.Property(t => t.preferred).HasColumnName("preferred");
            this.Property(t => t.contactLevel).HasColumnName("contactLevel");
            this.Property(t => t.datetime).HasColumnName("datetime");

        }
    }
}
