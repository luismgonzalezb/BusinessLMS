using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
	public class ToolMap : EntityTypeConfiguration<Tool>
	{
		public ToolMap()
		{
			// Primary Key
			this.HasKey(t => new { t.toolId, t.languageId });

			// Properties
			this.Property(t => t.toolId)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.Property(t => t.languageId)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			this.Property(t => t.name)
				.IsRequired()
				.HasMaxLength(50);

			// Table & Column Mappings
			this.ToTable("Tools");
			this.Property(t => t.toolId).HasColumnName("toolId");
			this.Property(t => t.languageId).HasColumnName("languageId");
			this.Property(t => t.name).HasColumnName("name");
			this.Property(t => t.def).HasColumnName("def");

		}
	}
}
