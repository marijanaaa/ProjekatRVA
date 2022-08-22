using ProjekatRVA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace ProjekatRVA.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id); //Podesavam primarni kljuc tabele
            builder.Property(x => x.Id).ValueGeneratedOnAdd(); //Kazem da ce se primarni kljuc
                                                               //automatski generisati prilikom dodavanja,
                                                               //redom 1 2 3...
            builder.Property(x => x.Name).HasMaxLength(30);//kazem da je maks duzina 30 karaktera
            builder.Property(x => x.LastName).HasMaxLength(30);
            builder.HasIndex(x => x.Username).IsUnique();
            builder.Property(x => x.Password).HasMaxLength(450);

            builder.HasMany(x => x.planners) //user ima vise planner-a
                  .WithOne(x => x.user);//planner ima jednog user-a
            builder.Property(x => x.IsLoggedIn);
            builder.Property(x => x.UserType);

        }
    }
}
