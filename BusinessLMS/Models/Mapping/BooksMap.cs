using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
    public class BooksMap : EntityTypeConfiguration<Book>
    {
        public BooksMap()
        {
            // Primary Key
            this.HasKey(t => t.BookId);

            this.Property(t => t.Title)
                .IsRequired();

            this.Property(t => t.Autor)
                .IsRequired();

            this.Property(t => t.IBONum)
                .IsRequired();

            this.Property(t => t.Link1);

            this.Property(t => t.Link2);

            this.Property(t => t.Link3);

            this.Property(t => t.priority)
                .IsRequired();

            this.Property(t => t.Count)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Books");
            this.Property(t => t.BookId).HasColumnName("BookId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Autor).HasColumnName("Autor");
            this.Property(t => t.IBONum).HasColumnName("IBONum");
            this.Property(t => t.Link1).HasColumnName("Link1");
            this.Property(t => t.Link2).HasColumnName("Link2");
            this.Property(t => t.Link3).HasColumnName("Link3");
            this.Property(t => t.Count).HasColumnName("Count");
            this.Property(t => t.priority).HasColumnName("Priority");
        }
    }
}
