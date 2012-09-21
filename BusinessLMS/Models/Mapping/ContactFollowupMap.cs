using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
    public class ContactFollowupMap : EntityTypeConfiguration<ContactFollowup>
    {
        public ContactFollowupMap()
        {
            // Primary Key
            this.HasKey(t => t.followupId);

            // Properties
            this.Property(t => t.IBONum)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.method)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ContactFollowups");
            this.Property(t => t.followupId).HasColumnName("followupId");
            this.Property(t => t.contactId).HasColumnName("contactId");
            this.Property(t => t.IBONum).HasColumnName("IBONum");
            this.Property(t => t.method).HasColumnName("method");
            this.Property(t => t.datetime).HasColumnName("datetime");

        }
    }
}
