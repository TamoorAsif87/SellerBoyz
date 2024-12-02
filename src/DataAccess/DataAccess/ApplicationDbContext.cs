using System.Reflection;

namespace DataAccess;
public class ApplicationDbContext:IdentityDbContext
{
    public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
    public DbSet<Product> Products =>  Set<Product>();
    public DbSet<Tags> Tags => Set<Tags>();
    public DbSet<ProductCollection> ProductCollections => Set<ProductCollection>();
    public DbSet<ProductTag> ProductTags => Set<ProductTag>();
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
        
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>().HavePrecision(10,2);
        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
