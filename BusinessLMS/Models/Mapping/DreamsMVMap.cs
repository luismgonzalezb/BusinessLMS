using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
	public class DreamsMVMap : EntityTypeConfiguration<DreamsMV>
	{
		public DreamsMVMap()
		{
			// Primary Key
			this.HasKey(t => t.dreamMVId);

			// Properties
			this.Property(t => t.IBONum)
				.IsRequired()
				.HasMaxLength(20);

			// Table & Column Mappings
			this.ToTable("DreamsMV");
			this.Property(t => t.dreamMVId).HasColumnName("dreamMVId");
			this.Property(t => t.IBONum).HasColumnName("IBONum");
			this.Property(t => t.mission).HasColumnName("mission");
			this.Property(t => t.vision).HasColumnName("vision");
			this.Property(t => t.purpose).HasColumnName("purpose");

		}
	}
}
