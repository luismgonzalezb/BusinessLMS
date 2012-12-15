using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
    public class AlertsMap : EntityTypeConfiguration<Alert>
    {
        public AlertsMap()
        {
            // Primary Key
            this.HasKey(t => t.AlertId);


            this.Property(t => t.AlertMsg)
                .IsRequired();

            this.Property(t => t.action)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.datetime)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Alerts");
            this.Property(t => t.AlertId).HasColumnName("AlertId");
            this.Property(t => t.AlertMsg).HasColumnName("AlertMsg");
            this.Property(t => t.action).HasColumnName("action");
            this.Property(t => t.datetime).HasColumnName("datetime");
        }
    }
}
