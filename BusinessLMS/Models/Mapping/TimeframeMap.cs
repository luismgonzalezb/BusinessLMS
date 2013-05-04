using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
	public class TimeframeMap : EntityTypeConfiguration<Timeframe>
	{
		public TimeframeMap()
		{
			// Primary Key
			this.HasKey(t => new { t.timeframeId, t.languageId });

			// Properties
			this.Property(t => t.timeframeId)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.Property(t => t.languageId)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			this.Property(t => t.title)
				.IsRequired()
				.HasMaxLength(50);

			// Table & Column Mappings
			this.ToTable("Timeframes");
			this.Property(t => t.timeframeId).HasColumnName("timeframeId");
			this.Property(t => t.languageId).HasColumnName("languageId");
			this.Property(t => t.title).HasColumnName("title");
			this.Property(t => t.days).HasColumnName("days");
			this.Property(t => t.timeLevel).HasColumnName("timeLevel");

		}
	}
}
