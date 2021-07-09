
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockInfo.Shared.Models;

namespace StockInfo.Server.EfConfigurations
{
    public class SavedStockEntityTypeConfiguration : IEntityTypeConfiguration<SavedStock>
    {
        public void Configure(EntityTypeBuilder<SavedStock> builder)
        {
            builder.HasKey(saved => new { saved.Username, saved.Ticker });

            builder.Property(saved => saved.Username).HasMaxLength(256);
            builder.Property(saved => saved.Ticker).HasMaxLength(50);

            builder.HasOne(saved => saved.Stock)
                .WithMany(stock => stock.Saved)
                .HasForeignKey(saved => saved.Ticker);
        }
    }
}
