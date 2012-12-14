using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
    public class LanguageMap : EntityTypeConfiguration<Language>
    {
        public LanguageMap()
        {
            // Primary Key
            this.HasKey(t => t.languageId);

            // Properties
            this.Property(t => t.language1)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Languages");
            this.Property(t => t.languageId).HasColumnName("languageId");
            this.Property(t => t.language1).HasColumnName("language");
        }
    }
}
