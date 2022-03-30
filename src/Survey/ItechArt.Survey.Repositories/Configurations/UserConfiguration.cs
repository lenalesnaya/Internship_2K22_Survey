using ItechArt.Survey.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItechArt.Survey.Repositories.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(u => u.UserName)
            .HasMaxLength(User.UserNameMaxLength)
            .IsRequired();
        builder
            .HasIndex(u => u.UserName)
            .IsUnique();

        builder
            .Property(u => u.NormalizedUserName)
            .HasMaxLength(User.UserNameMaxLength)
            .IsRequired();
        builder
            .HasIndex(u => u.NormalizedUserName)
            .IsUnique();

        builder
            .Property(u => u.Email)
            .HasMaxLength(User.EmailMaxLength)
            .IsRequired();
        builder
            .HasIndex(u => u.Email)
            .IsUnique();

        builder
            .Property(u => u.NormalizedEmail)
            .HasMaxLength(User.EmailMaxLength)
            .IsRequired();
        builder
            .HasIndex(u => u.NormalizedEmail)
            .IsUnique();

        builder
            .Property(u => u.PasswordHash)
            .IsRequired();

        builder
            .HasData(new User
            {
                Id = 1,
                UserName = User.UserAdmin,
                NormalizedUserName = User.UserAdmin.ToUpper(),
                Email = "admin@mail.ru",
                NormalizedEmail = "ADMIN@MAIL.RU",
                PasswordHash = "AQAAAAEAACcQAAAAEIgYyAtD5WcSUQEn36VGTHQ/JThoBBhcNxyBe++i1eISwktB0UJAQXLvtUbin2Qf3A==",
                ConcurrencyStamp = "85263788-277f-4f89-b8c4-a11ac465ed58"
            });
    }
}