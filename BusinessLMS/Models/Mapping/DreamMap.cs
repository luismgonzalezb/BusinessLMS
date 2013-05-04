using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
	public class DreamMap : EntityTypeConfiguration<Dream>
	{
		public DreamMap()
		{
			// Primary Key
			this.HasKey(t => t.dreamId);

			// Properties
			this.Property(t => t.IBONum)
				.IsRequired()
				.HasMaxLength(20);

			this.Property(t => t.dream1)
				.IsRequired()
				.HasMaxLength(250);

			this.Property(t => t.picture)
				.HasMaxLength(250);

			// Table & Column Mappings
			this.ToTable("Dreams");
			this.Property(t => t.dreamId).HasColumnName("dreamId");
			this.Property(t => t.IBONum).HasColumnName("IBONum");
			this.Property(t => t.timeframeId).HasColumnName("timeframeId");
			this.Property(t => t.areaId).HasColumnName("areaId");
			this.Property(t => t.dream1).HasColumnName("dream");
			this.Property(t => t.dreamLevel).HasColumnName("dreamLevel");
			this.Property(t => t.achieved).HasColumnName("achieved");
			this.Property(t => t.datetime).HasColumnName("datetime");
			this.Property(t => t.picture).HasColumnName("picture");

		}
	}
}
