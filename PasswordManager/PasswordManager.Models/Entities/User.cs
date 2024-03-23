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

        public ICollection<Info> MyInfos = new List<Info>();

        public ICollection<Info> SharedInfos = new List<Info>();

        public ICollection<FriendRequest> SentFriendRequests = new List<FriendRequest>();

        public ICollection<FriendRequest> ReceivedFriendRequests = new List<FriendRequest>();

        public ICollection<Friendship> Friendships = new List<Friendship>();
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
