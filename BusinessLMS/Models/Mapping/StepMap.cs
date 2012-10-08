using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
    public class StepMap : EntityTypeConfiguration<Step>
    {
        public StepMap()
        {
            // Primary Key
            this.HasKey(t => t.stepId);

            // Properties
            this.Property(t => t.title)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.description)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.iconClass)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.action)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.controller)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Steps");
            this.Property(t => t.stepId).HasColumnName("stepId");
            this.Property(t => t.parentStepId).HasColumnName("parentStepId");
            this.Property(t => t.title).HasColumnName("title");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.stepOrder).HasColumnName("stepOrder");
            this.Property(t => t.iconClass).HasColumnName("iconClass");
            this.Property(t => t.action).HasColumnName("action");
            this.Property(t => t.controller).HasColumnName("controller");
        }
    }
}
