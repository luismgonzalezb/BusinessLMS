using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
	public class StepsTextMap : EntityTypeConfiguration<StepsText>
	{
		public StepsTextMap()
		{
			// Primary Key
			this.HasKey(t => new { t.stepId, t.languageId });

			// Properties
			this.Property(t => t.stepId)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			this.Property(t => t.languageId)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			this.Property(t => t.text)
				.IsRequired()
				.HasMaxLength(50);

			this.Property(t => t.description)
				.IsRequired()
				.HasMaxLength(250);

			// Table & Column Mappings
			this.ToTable("StepsText");
			this.Property(t => t.stepId).HasColumnName("stepId");
			this.Property(t => t.languageId).HasColumnName("languageId");
			this.Property(t => t.text).HasColumnName("text");
			this.Property(t => t.description).HasColumnName("description");

		}
	}
}
