using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
    public class CountryMap : EntityTypeConfiguration<Country>
    {
        public CountryMap()
        {
            // Primary Key
            this.HasKey(t => t.countryId);

            // Properties
            this.Property(t => t.country1)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Countries");
            this.Property(t => t.countryId).HasColumnName("countryId");
            this.Property(t => t.country1).HasColumnName("country");
        }
    }
}
