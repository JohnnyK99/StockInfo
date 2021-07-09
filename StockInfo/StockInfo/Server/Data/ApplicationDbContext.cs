using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StockInfo.Server.EfConfigurations;
using StockInfo.Server.Models;
using StockInfo.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockInfo.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<StockDetails> Stocks { get; set; }
        public DbSet<StockValue> StockValues { get; set; }
        public DbSet<SavedStock> SavedStocks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new StockDetailsEntityTypeConfiguration());
            builder.ApplyConfiguration(new StockValueEntityTypeConfiguration());
            builder.ApplyConfiguration(new SavedStockEntityTypeConfiguration());
        }
    }
}
