using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
    public class AlertMap : EntityTypeConfiguration<Alert>
    {
        public AlertMap()
        {
            // Primary Key
            this.HasKey(t => t.AlertId);

            // Properties
            this.Property(t => t.AlertId)
                .IsRequired()
                .HasMaxLength(38);

            this.Property(t => t.AlertMsg)
                .IsRequired();

            this.Property(t => t.action)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Alerts");
            this.Property(t => t.AlertId).HasColumnName("AlertId");
            this.Property(t => t.AlertMsg).HasColumnName("AlertMsg");
            this.Property(t => t.action).HasColumnName("action");
            this.Property(t => t.datetime).HasColumnName("datetime");
        }
    }
}
