using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
	public class AreaMap : EntityTypeConfiguration<Area>
	{
		public AreaMap()
		{
			// Primary Key
			this.HasKey(t => new { t.areaId, t.languageId });

			// Properties
			this.Property(t => t.areaId)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.Property(t => t.languageId)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			this.Property(t => t.title)
				.IsRequired()
				.HasMaxLength(50);

			// Table & Column Mappings
			this.ToTable("Areas");
			this.Property(t => t.areaId).HasColumnName("areaId");
			this.Property(t => t.languageId).HasColumnName("languageId");
			this.Property(t => t.title).HasColumnName("title");

		}
	}
}
