using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockInfo.Shared.Models;

namespace StockInfo.Server.EfConfigurations
{
    public class StockDetailsEntityTypeConfiguration : IEntityTypeConfiguration<StockDetails>
    {
        public void Configure(EntityTypeBuilder<StockDetails> builder)
        {
            builder.HasKey(stock => stock.Ticker);
            builder.Property(stock => stock.Ticker).HasMaxLength(50);

            builder.Property(stock => stock.Name).IsRequired().HasMaxLength(100);
            builder.Property(stock => stock.Description).HasMaxLength(10000);
            builder.Property(stock => stock.Country).HasMaxLength(100);
            builder.Property(stock => stock.Industry).HasMaxLength(100);
            builder.Property(stock => stock.Sector).HasMaxLength(100);
            builder.Property(stock => stock.Exchange).HasMaxLength(100);
            builder.Property(stock => stock.Currency).HasMaxLength(3);
        }
    }
}

