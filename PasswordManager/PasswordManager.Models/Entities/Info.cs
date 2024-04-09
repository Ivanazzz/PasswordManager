using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace PasswordManager.Models.Entities
{
    public class Info
    {
        public int Id { get; set; }

        public string Website { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsShared { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }

    public class InfoConfiguration : IEntityTypeConfiguration<Info>
    {
        public void Configure(EntityTypeBuilder<Info> builder)
        {
            builder
                .Property(b => b.Website)
                .IsRequired();

            builder
                .Property(b => b.Username)
                .IsRequired();

            builder
                .Property(b => b.Password)
                .IsRequired();

            builder
                .Property(b => b.CreateDate)
                .IsRequired();

            builder
                .Property(b => b.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder
                .Property(b => b.IsShared)
                .IsRequired()
                .HasDefaultValue(false);

            builder
                .Property(b => b.UserId)
                .IsRequired();
        }
    }
}
