using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
    public class TicketMap : EntityTypeConfiguration<Ticket>
    {
        public TicketMap()
        {
            // Primary Key
            this.HasKey(t => t.ticketId);

            // Properties
            //this.Property(t => t.dreamId)
            //    .HasDatabaseGeneratedOption();

            this.Property(t => t.ticketType)
                .IsRequired();

            this.Property(t => t.IBONum)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.description)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.datetime)
                .IsRequired();

            this.Property(t => t.priority)
                .IsRequired();

            this.Property(t => t.solved)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Tickets");
            this.Property(t => t.ticketId).HasColumnName("ticketId");
            this.Property(t => t.ticketType).HasColumnName("ticketType");
            this.Property(t => t.IBONum).HasColumnName("IBONum");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.datetime).HasColumnName("datetime");
            this.Property(t => t.priority).HasColumnName("priority");
            this.Property(t => t.solved).HasColumnName("solved");
            this.Property(t => t.developer).HasColumnName("developer");
            this.Property(t => t.impact).HasColumnName("impact");
        }
    }
}
