using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
	public class ClearSystemCookiesMap : EntityTypeConfiguration<ClearSystemCookies>
	{
		public ClearSystemCookiesMap()
		{
			// Primary Key
			this.HasKey(t => t.clearId);

			// Table & Column Mappings
			this.ToTable("ClearSystemCookies");
			this.Property(t => t.clearId).HasColumnName("clearId");
			this.Property(t => t.Clear).HasColumnName("Clear");
		}
	}
}
