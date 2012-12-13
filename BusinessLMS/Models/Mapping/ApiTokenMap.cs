using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
    public class ApiTokenMap : EntityTypeConfiguration<ApiToken>
    {
        public ApiTokenMap()
        {
            // Primary Key
            this.HasKey(t => t.apiName);

            // Properties
            this.Property(t => t.apiName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.apiKey)
                .IsRequired()
                .HasMaxLength(38);

            // Table & Column Mappings
            this.ToTable("ApiTokens");
            this.Property(t => t.apiName).HasColumnName("apiName");
            this.Property(t => t.apiKey).HasColumnName("apiKey");
        }
    }
}
