using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BusinessLMS.Models.Mapping
{
    public class StateMap : EntityTypeConfiguration<State>
    {
        public StateMap()
        {
            // Primary Key
            this.HasKey(t => t.StateCode);

            // Properties
            this.Property(t => t.StateCode)
                .IsRequired()
                .HasMaxLength(3);

            this.Property(t => t.StateAbbreviation)
                .HasMaxLength(2);

            this.Property(t => t.StateName)
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("States");
            this.Property(t => t.StateCode).HasColumnName("StateCode");
            this.Property(t => t.StateAbbreviation).HasColumnName("StateAbbreviation");
            this.Property(t => t.StateName).HasColumnName("StateName");
        }
    }
}
