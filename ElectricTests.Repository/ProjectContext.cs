using System.Data.Entity;
using ElectricTests.Model;

namespace ElectricTests.Repository
{
	public class ProjectContext : DbContext {
		public DbSet<FormattedDocument> FormattedDocuments { get; set; }
		public DbSet<Section> Sections { get; set; }
		public DbSet<Paragraph> Paragraphs { get; set; }
		public DbSet<Test> Tests { get; set; }
		public DbSet<Question> Questions { get; set; }
		public DbSet<UserProfile> UserProfiles { get; set; }

		protected override void OnModelCreating (DbModelBuilder modelBuilder) {

			//Create many-to-many type table TestsQuestions
			modelBuilder.Entity<Test>()
				.HasMany<Question>(q => q.Questions)
				.WithMany(t => t.Tests)
				.Map(m => {
					m.MapLeftKey("TestId");
					m.MapRightKey("QuestionId");
					m.ToTable("TestsQuestions");
				});
		}
	}
}
