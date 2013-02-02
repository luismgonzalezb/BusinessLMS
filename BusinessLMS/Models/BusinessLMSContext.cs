using System.Data.Entity;
using BusinessLMS.Models.Mapping;

namespace BusinessLMS.Models
{
	public class BusinessLMSContext : DbContext
	{
		static BusinessLMSContext()
		{
			Database.SetInitializer<BusinessLMSContext>(null);
		}

		public BusinessLMSContext()
			: base("Name=BusinessLMSContext")
		{
		}

		public DbSet<Area> Areas { get; set; }
		public DbSet<CompletedStep> CompletedSteps { get; set; }
		public DbSet<ContactFollowup> ContactFollowups { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<ContactType> ContactTypes { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<Dream> Dreams { get; set; }
		public DbSet<DreamMV> DreamsMV { get; set; }
		public DbSet<Goal> Goals { get; set; }
		public DbSet<IBO> IBOs { get; set; }
		public DbSet<Language> Languages { get; set; }
		public DbSet<Location> Locations { get; set; }
		public DbSet<State> States { get; set; }
		public DbSet<Step> Steps { get; set; }
		public DbSet<Timeframe> Timeframes { get; set; }
		public DbSet<Tool> Tools { get; set; }
		public DbSet<ZIPCode> ZIPCodes { get; set; }
		public DbSet<Ticket> Tickets { get; set; }
		public DbSet<ApiToken> ApiTokens { get; set; }
		public DbSet<Alert> Alerts { get; set; }
		public DbSet<AlertIBO> AlertsIBO { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<ClearSystemCookies> ClearSystemCookies { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new AreaMap());
			modelBuilder.Configurations.Add(new CompletedStepMap());
			modelBuilder.Configurations.Add(new ContactFollowupMap());
			modelBuilder.Configurations.Add(new ContactMap());
			modelBuilder.Configurations.Add(new ContactTypeMap());
			modelBuilder.Configurations.Add(new CountryMap());
			modelBuilder.Configurations.Add(new DreamMap());
			modelBuilder.Configurations.Add(new DreamMVMap());
			modelBuilder.Configurations.Add(new GoalMap());
			modelBuilder.Configurations.Add(new IBOMap());
			modelBuilder.Configurations.Add(new LanguageMap());
			modelBuilder.Configurations.Add(new LocationMap());
			modelBuilder.Configurations.Add(new StateMap());
			modelBuilder.Configurations.Add(new StepMap());
			modelBuilder.Configurations.Add(new TimeframeMap());
			modelBuilder.Configurations.Add(new ToolMap());
			modelBuilder.Configurations.Add(new ZIPCodeMap());
			modelBuilder.Configurations.Add(new TicketMap());
			modelBuilder.Configurations.Add(new ApiTokenMap());
			modelBuilder.Configurations.Add(new AlertsMap());
			modelBuilder.Configurations.Add(new AlertsIBOMap());
			modelBuilder.Configurations.Add(new BooksMap());
			modelBuilder.Configurations.Add(new ClearSystemCookiesMap());
		}

		


	}
}
