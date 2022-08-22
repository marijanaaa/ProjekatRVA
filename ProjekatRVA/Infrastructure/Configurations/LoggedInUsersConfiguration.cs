using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjekatRVA.Models;

namespace ProjekatRVA.Infrastructure.Configurations
{
    public class LoggedInUsersConfiguration : IEntityTypeConfiguration<LoggedInUsers>
    {
        public void Configure(EntityTypeBuilder<LoggedInUsers> builder)
        {
            builder.HasKey(x => x.Id); //Podesavam primarni kljuc tabele
            builder.Property(x => x.Id).ValueGeneratedOnAdd(); //generise se random id
            builder.Property(x => x.Token);
            builder.Property(x=>x.UserId);
        }
    }
}
