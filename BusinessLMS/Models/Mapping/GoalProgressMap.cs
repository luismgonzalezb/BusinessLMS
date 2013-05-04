using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
	public class GoalProgressMap : EntityTypeConfiguration<GoalProgress>
	{
		public GoalProgressMap()
		{
			// Primary Key
			this.HasKey(t => t.progressId);

			// Properties
			// Table & Column Mappings
			this.ToTable("GoalProgress");
			this.Property(t => t.progressId).HasColumnName("progressId");
			this.Property(t => t.goalId).HasColumnName("goalId");
			this.Property(t => t.progress).HasColumnName("progress");
			this.Property(t => t.datetime).HasColumnName("datetime");

		}
	}
}
