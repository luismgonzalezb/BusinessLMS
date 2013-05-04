using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
	public class IBOMap : EntityTypeConfiguration<IBO>
	{
		public IBOMap()
		{
			// Primary Key
			this.HasKey(t => t.IBONum);

			// Properties
			this.Property(t => t.IBONum)
				.IsRequired()
				.HasMaxLength(20);

			this.Property(t => t.UPLine)
				.HasMaxLength(20);

			this.Property(t => t.firstName)
				.IsRequired()
				.HasMaxLength(50);

			this.Property(t => t.lastName)
				.IsRequired()
				.HasMaxLength(50);

			this.Property(t => t.accesstoken)
				.HasMaxLength(250);

			this.Property(t => t.email)
				.IsRequired()
				.HasMaxLength(100);

			this.Property(t => t.facebookid)
				.HasMaxLength(250);

			this.Property(t => t.twitter)
				.IsRequired()
				.HasMaxLength(20);

			this.Property(t => t.picture)
				.HasMaxLength(250);

			this.Property(t => t.phone)
				.HasMaxLength(20);

			// Table & Column Mappings
			this.ToTable("IBOs");
			this.Property(t => t.IBONum).HasColumnName("IBONum");
			this.Property(t => t.UPLine).HasColumnName("UPLine");
			this.Property(t => t.languageId).HasColumnName("languageId");
			this.Property(t => t.firstName).HasColumnName("firstName");
			this.Property(t => t.lastName).HasColumnName("lastName");
			this.Property(t => t.accesstoken).HasColumnName("accesstoken");
			this.Property(t => t.email).HasColumnName("email");
			this.Property(t => t.facebookid).HasColumnName("facebookid");
			this.Property(t => t.twitter).HasColumnName("twitter");
			this.Property(t => t.datetime).HasColumnName("datetime");
			this.Property(t => t.picture).HasColumnName("picture");
			this.Property(t => t.UserId).HasColumnName("UserId");
			this.Property(t => t.birthday).HasColumnName("birthday");
			this.Property(t => t.phone).HasColumnName("phone");
			this.Property(t => t.level).HasColumnName("level");
			this.Property(t => t.newsletteroptin).HasColumnName("newsletteroptin");

		}
	}
}
