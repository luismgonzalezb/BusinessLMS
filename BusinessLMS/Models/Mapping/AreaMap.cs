using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
    public class AreaMap : EntityTypeConfiguration<Area>
    {
        public AreaMap()
        {
            // Primary Key
            this.HasKey(t => t.areaId);

            // Properties
            this.Property(t => t.title)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Areas");
            this.Property(t => t.areaId).HasColumnName("areaId");
            this.Property(t => t.title).HasColumnName("title");
        }
    }
}
