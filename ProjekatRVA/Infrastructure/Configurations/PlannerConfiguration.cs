using ProjekatRVA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjekatRVA.Infrastructure.Configurations
{
    public class PlannerConfiguration : IEntityTypeConfiguration<Planner>
    {
        public void Configure(EntityTypeBuilder<Planner> builder)
        {
            builder.HasKey(x => x.Id); //Podesavam primarni kljuc tabele
            builder.Property(x => x.Id).ValueGeneratedOnAdd(); //generise se random id
            builder.HasOne(x => x.user) //Kazemo da planner ima jednog user-a
                  .WithMany(x => x.planners) // A jedan user vise planner-a
                  .HasForeignKey(x => x.userId) // Strani kljuc  je userId
                  .OnDelete(DeleteBehavior.Cascade);// Ako se obrise user kaskadno se brisu svi njegovi planner-i

            builder.HasMany(x => x.events); //user ima vise planner-a
            builder.Property(x => x.PlannerName).HasMaxLength(100);
            builder.Property(x => x.Time);
        }
    }
}
