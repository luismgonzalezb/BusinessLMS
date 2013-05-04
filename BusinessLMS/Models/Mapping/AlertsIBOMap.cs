using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
	public class AlertsIBOMap : EntityTypeConfiguration<AlertsIBO>
	{
		public AlertsIBOMap()
		{
			// Primary Key
			this.HasKey(t => new { t.AlertId, t.IBONum });

			// Properties
			this.Property(t => t.AlertId)
				.IsRequired()
				.HasMaxLength(38);

			this.Property(t => t.IBONum)
				.IsRequired()
				.HasMaxLength(20);

			// Table & Column Mappings
			this.ToTable("AlertsIBO");
			this.Property(t => t.AlertId).HasColumnName("AlertId");
			this.Property(t => t.IBONum).HasColumnName("IBONum");
			this.Property(t => t.datetime).HasColumnName("datetime");

		}
	}
}
