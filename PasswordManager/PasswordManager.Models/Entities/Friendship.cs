using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace PasswordManager.Models.Entities
{
    public class Friendship
    {
        public int Id { get; set; }

        public int FirstUserId { get; set; }

        public int SecondUserId { get; set; }

        public User FirstUser { get; set; }

        public User SecondUser { get; set; }
    }

    public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasKey(friendship => new 
            { 
                friendship.FirstUserId, 
                friendship.SecondUserId 
            });

            builder
                .Property(b => b.Id)
                .IsRequired();

            builder
                .Property(b => b.FirstUserId)
                .IsRequired();

            builder
                .Property(b => b.SecondUserId)
                .IsRequired();

            builder
                .HasOne(friendship => friendship.FirstUser)
                .WithMany()
                .HasForeignKey(friendship => friendship.FirstUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(friendship => friendship.SecondUser)
                .WithMany()
                .HasForeignKey(friendship => friendship.SecondUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
