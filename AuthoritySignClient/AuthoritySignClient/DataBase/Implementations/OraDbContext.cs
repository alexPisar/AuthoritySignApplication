using AuthoritySignClient.DataBase.DataBaseObjects;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace AuthoritySignClient.DataBase.Implementations
{
    internal class OraDbContext : DbContext
    {
        public OraDbContext(string connectionString):base(connectionString)
        {
            Configure();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefCustomer>().HasKey(r => r.Id).ToTable("REF_CUSTOMERS", "ABT")
                .Property(r => r.Id).HasColumnName(@"ID").IsRequired();

            modelBuilder.Entity<RefAuthoritySignDocuments>().HasKey(r => new { r.IdCustomer, r.Inn }).ToTable("REF_AUTHORITY_SIGN_DOCUMENTS", "EDI")
                .Property(r => r.IdCustomer).HasColumnName(@"ID_CUSTOMER").IsRequired();
        }

        private void Configure()
        {
            this.Configuration.AutoDetectChangesEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.ValidateOnSaveEnabled = true;
            this.Database.CommandTimeout = 300;
            
        }

        public DbSet<RefCustomer> RefCustomers { get; set; }
        public DbSet<RefAuthoritySignDocuments> RefAuthoritySignDocuments { get; set; }
    }
}
