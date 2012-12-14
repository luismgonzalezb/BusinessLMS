using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
    public class ContactTypeMap : EntityTypeConfiguration<ContactType>
    {
        public ContactTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.contactTypeId);

            // Properties
            this.Property(t => t.type)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ContactTypes");
            this.Property(t => t.contactTypeId).HasColumnName("contactTypeId");
            this.Property(t => t.type).HasColumnName("type");
        }
    }
}
