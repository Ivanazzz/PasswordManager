using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace PasswordManager.Models.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Info> Infos { get; set; } = new List<Info>();

        public ICollection<FriendRequest> SentFriendRequests { get; set; } = new List<FriendRequest>();

        public ICollection<FriendRequest> ReceivedFriendRequests { get; set; } = new List<FriendRequest>();
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(b => b.Username)
                .IsRequired();

            builder
                .Property(b => b.Email)
                .IsRequired();

            builder
                .HasIndex(b => b.Email)
                .IsUnique();

            builder
                .Property(b => b.PasswordHash)
                .IsRequired();

            builder
                .Property(b => b.PasswordSalt)
                .IsRequired();

            builder
                .Property(b => b.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
