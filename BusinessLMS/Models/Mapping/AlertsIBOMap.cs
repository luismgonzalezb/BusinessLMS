using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
    public class AlertsIBOMap : EntityTypeConfiguration<AlertIBO>
    {
        public AlertsIBOMap()
        {
            // Primary Key
            this.HasKey(t => new { t.AlertId, t.IBONum });

            this.Property(t => t.datetime)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("AlertsIBO");
            this.Property(t => t.AlertId).HasColumnName("AlertId");
            this.Property(t => t.IBONum).HasColumnName("IBONum");
            this.Property(t => t.datetime).HasColumnName("datetime");
        }
    }
}
