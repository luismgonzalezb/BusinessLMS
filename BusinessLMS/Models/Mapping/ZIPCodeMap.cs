using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
    public class ZIPCodeMap : EntityTypeConfiguration<ZIPCode>
    {
        public ZIPCodeMap()
        {
            // Primary Key
            this.HasKey(t => t.ZIPCode1);

            // Properties
            this.Property(t => t.ZIPCode1)
                .IsRequired()
                .HasMaxLength(5);

            this.Property(t => t.Latitude)
                .HasMaxLength(10);

            this.Property(t => t.Longitude)
                .HasMaxLength(10);

            this.Property(t => t.Class)
                .HasMaxLength(1);

            this.Property(t => t.City)
                .HasMaxLength(28);

            this.Property(t => t.StateCode)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("ZIPCodes");
            this.Property(t => t.ZIPCode1).HasColumnName("ZIPCode");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.Class).HasColumnName("Class");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.StateCode).HasColumnName("StateCode");

        }
    }
}
