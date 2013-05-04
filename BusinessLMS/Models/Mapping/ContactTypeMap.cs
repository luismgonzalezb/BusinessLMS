using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
	public class ContactTypeMap : EntityTypeConfiguration<ContactType>
	{
		public ContactTypeMap()
		{
			// Primary Key
			this.HasKey(t => new { t.contactTypeId, t.languageId });

			// Properties
			this.Property(t => t.contactTypeId)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.Property(t => t.languageId)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			this.Property(t => t.type)
				.IsRequired()
				.HasMaxLength(50);

			// Table & Column Mappings
			this.ToTable("ContactTypes");
			this.Property(t => t.contactTypeId).HasColumnName("contactTypeId");
			this.Property(t => t.languageId).HasColumnName("languageId");
			this.Property(t => t.type).HasColumnName("type");

		}
	}
}
