using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
    public class CompletedStepMap : EntityTypeConfiguration<CompletedStep>
    {
        public CompletedStepMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IBONum, t.stepId });

            // Properties
            this.Property(t => t.IBONum)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.stepId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CompletedSteps");
            this.Property(t => t.IBONum).HasColumnName("IBONum");
            this.Property(t => t.stepId).HasColumnName("stepId");
            this.Property(t => t.datetime).HasColumnName("datetime");

        }
    }
}
