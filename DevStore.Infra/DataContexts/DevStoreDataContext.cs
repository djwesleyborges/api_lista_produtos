using DevStore.Domain;
using DevStore.Infra.Mappings;
using System.Data.Entity;

namespace DevStore.Infra.DataContexts
{
    public class DevStoreDataContext : DbContext // Fazendo conexão com banco usando Entity
    {
        public DevStoreDataContext()
            : base("Data Source=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=DevStore;Data Source=N0006181") //  <- conection string
        {
            //Database.SetInitializer<DevStoreDataContext>(new DevStoreDataContextInitializer());
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class DevStoreDataContextInitializer : DropCreateDatabaseIfModelChanges<DevStoreDataContext> 
    {
        protected override void Seed(DevStoreDataContext context)
        {
            context.Categories.Add(new Category { Id = 1, Title = "Informatica" });
            context.Categories.Add(new Category { Id = 1, Title = "Games" });
            context.Categories.Add(new Category { Id = 1, Title = "Papelaria" });
            context.SaveChanges(); // commit

            context.Products.Add(new Product { Id = 1, Title="Mouse", Price=10, IsActive=true, CategoryId=1});

            context.Products.Add(new Product { Id = 1, Title = "Mouse", Price = 20, CategoryId = 1, IsActive = true });
            context.Products.Add(new Product { Id = 2, Title = "Teclado", Price = 20, CategoryId = 1, IsActive = true });
            context.Products.Add(new Product { Id = 3, Title = "Nobreak", Price = 20, CategoryId = 1, IsActive = true });
            context.Products.Add(new Product { Id = 4, Title = "Mortal Kombat", CategoryId = 2, Price = 20, IsActive = true });
            context.Products.Add(new Product { Id = 5, Title = "Call of Duty", CategoryId = 2, Price = 20, IsActive = true });
            context.Products.Add(new Product { Id = 6, Title = "Marvel vs Capcon", CategoryId = 2, Price = 20, IsActive = true });
            context.Products.Add(new Product { Id = 7, Title = "Caderno", CategoryId = 3, Price = 20, IsActive = true });
            context.SaveChanges(); // commit
        }
    }
}
