using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
    public class LocationMap : EntityTypeConfiguration<Location>
    {
        public LocationMap()
        {
            // Primary Key
            this.HasKey(t => t.locationId);

            // Properties
            this.Property(t => t.IBONum)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ZIPCode)
                .IsRequired()
                .HasMaxLength(5);

            this.Property(t => t.address)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.address2)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Locations");
            this.Property(t => t.locationId).HasColumnName("locationId");
            this.Property(t => t.IBONum).HasColumnName("IBONum");
            this.Property(t => t.countryId).HasColumnName("countryId");
            this.Property(t => t.ZIPCode).HasColumnName("ZIPCode");
            this.Property(t => t.address).HasColumnName("address");
            this.Property(t => t.address2).HasColumnName("address2");

        }
    }
}
