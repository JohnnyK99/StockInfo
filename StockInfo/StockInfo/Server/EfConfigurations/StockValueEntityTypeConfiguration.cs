using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockInfo.Shared.Models;

namespace StockInfo.Server.EfConfigurations
{
    public class StockValueEntityTypeConfiguration : IEntityTypeConfiguration<StockValue>
    {
        public void Configure(EntityTypeBuilder<StockValue> builder)
        {
            builder.HasKey(value => new { value.Ticker, value.Date });

            builder.Property(value => value.Ticker).HasMaxLength(50);

            builder.HasOne(value => value.Stock)
                .WithMany(stock => stock.Values)
                .HasForeignKey(value => value.Ticker);
        }
    }
}
