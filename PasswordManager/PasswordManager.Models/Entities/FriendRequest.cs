using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace PasswordManager.Models.Entities
{
    public class FriendRequest
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public bool IsAccepted { get; set; }

        public User Sender { get; set; }

        public User Receiver { get; set; }
    }

    public class FriendRequestConfiguration : IEntityTypeConfiguration<FriendRequest>
    {
        public void Configure(EntityTypeBuilder<FriendRequest> builder)
        {
            builder
                .Property(b => b.Id)
                .IsRequired();

            builder
                .Property(b => b.SenderId)
                .IsRequired();

            builder
                .Property(b => b.ReceiverId)
                .IsRequired();

            builder
                .Property(b => b.IsAccepted)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
