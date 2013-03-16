using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
    public class ProgressMap : EntityTypeConfiguration<Progress>
    {
        public ProgressMap()
        {
            // Primary Key
            this.HasKey(t => t.ProgressId);

            this.Property(t => t.GoalId)
                .IsRequired();

            this.Property(t => t.progress)
                .IsRequired();

            this.Property(t => t.datetime)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("GoalProgress");
            this.Property(t => t.ProgressId).HasColumnName("progressId");
            this.Property(t => t.GoalId).HasColumnName("goalId");
            this.Property(t => t.progress).HasColumnName("progress");
            this.Property(t => t.datetime).HasColumnName("datetime");
        }
    }
}
