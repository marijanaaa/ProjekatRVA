using ProjekatRVA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjekatRVA.Infrastructure.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(x => x.Id); //Podesavam primarni kljuc tabele
            builder.Property(x => x.Id).ValueGeneratedOnAdd(); //Kazem da ce se primarni kljuc
            builder.Property(x => x.Text).HasMaxLength(100);//kazem da je maks duzina 30 karaktera
            builder.Property(x => x.DateAndTime).HasMaxLength(10);
            builder.HasOne(x => x.planner)
                .WithMany(x => x.events)
                .HasForeignKey(x => x.plannerId) // Strani kljuc  je plannerId
                .OnDelete(DeleteBehavior.Cascade);// ako se izbrise planner nema ni event-a
            builder.Property(x => x.Time);
        }
    }
}
