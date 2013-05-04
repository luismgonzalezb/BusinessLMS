using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
	public class BookMap : EntityTypeConfiguration<Book>
	{
		public BookMap()
		{
			// Primary Key
			this.HasKey(t => t.BookId);

			// Properties
			this.Property(t => t.Title)
				.IsRequired()
				.HasMaxLength(255);

			this.Property(t => t.Autor)
				.IsRequired()
				.HasMaxLength(255);

			this.Property(t => t.IBONum)
				.IsRequired()
				.HasMaxLength(20);

			this.Property(t => t.Link1)
				.HasMaxLength(255);

			this.Property(t => t.Link2)
				.HasMaxLength(255);

			this.Property(t => t.Link3)
				.HasMaxLength(255);

			// Table & Column Mappings
			this.ToTable("Books");
			this.Property(t => t.BookId).HasColumnName("BookId");
			this.Property(t => t.Title).HasColumnName("Title");
			this.Property(t => t.Autor).HasColumnName("Autor");
			this.Property(t => t.IBONum).HasColumnName("IBONum");
			this.Property(t => t.Link1).HasColumnName("Link1");
			this.Property(t => t.Link2).HasColumnName("Link2");
			this.Property(t => t.Link3).HasColumnName("Link3");
			this.Property(t => t.Priority).HasColumnName("Priority");
			this.Property(t => t.Count).HasColumnName("Count");


		}
	}
}
