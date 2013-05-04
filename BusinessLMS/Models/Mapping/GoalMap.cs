using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
	public class GoalMap : EntityTypeConfiguration<Goal>
	{
		public GoalMap()
		{
			// Primary Key
			this.HasKey(t => t.goalId);

			// Properties
			this.Property(t => t.picture)
				.HasMaxLength(250);

			// Table & Column Mappings
			this.ToTable("Goals");
			this.Property(t => t.goalId).HasColumnName("goalId");
			this.Property(t => t.dreamId).HasColumnName("dreamId");
			this.Property(t => t.timeframeId).HasColumnName("timeframeId");
			this.Property(t => t.toolId).HasColumnName("toolId");
			this.Property(t => t.goal1).HasColumnName("goal");
			this.Property(t => t.goalLevel).HasColumnName("goalLevel");
			this.Property(t => t.completed).HasColumnName("completed");
			this.Property(t => t.datetime).HasColumnName("datetime");
			this.Property(t => t.picture).HasColumnName("picture");

		}
	}
}
