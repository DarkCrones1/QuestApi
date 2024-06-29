using Microsoft.EntityFrameworkCore;

using QuestApi.Entities;

namespace QuestApi.Data;

public partial class QuestDbContext : DbContext
{
    public QuestDbContext()
    {
    }

    public QuestDbContext(DbContextOptions<QuestDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Quest> Quest { get; set; }

    public virtual DbSet<Answer> Answer { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            option => option.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuestDbContext).Assembly);
    }
}