﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PasswordManager.Models;

#nullable disable

namespace PasswordManager.Models.Migrations
{
    [DbContext(typeof(PasswordManagerDbContext))]
    partial class PasswordManagerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PasswordManager.Models.Entities.FriendRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsAccepted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<int>("ReceiverId")
                        .HasColumnType("integer");

                    b.Property<int>("SenderId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("FriendRequests");
                });

            modelBuilder.Entity("PasswordManager.Models.Entities.Friendship", b =>
                {
                    b.Property<int>("FirstUserId")
                        .HasColumnType("integer");

                    b.Property<int>("SecondUserId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.HasKey("FirstUserId", "SecondUserId");

                    b.HasIndex("SecondUserId");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("PasswordManager.Models.Entities.Info", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsShared")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Infos");
                });

            modelBuilder.Entity("PasswordManager.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PasswordManager.Models.Entities.FriendRequest", b =>
                {
                    b.HasOne("PasswordManager.Models.Entities.User", "Receiver")
                        .WithMany("ReceivedFriendRequests")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PasswordManager.Models.Entities.User", "Sender")
                        .WithMany("SentFriendRequests")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("PasswordManager.Models.Entities.Friendship", b =>
                {
                    b.HasOne("PasswordManager.Models.Entities.User", "FirstUser")
                        .WithMany()
                        .HasForeignKey("FirstUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PasswordManager.Models.Entities.User", "SecondUser")
                        .WithMany()
                        .HasForeignKey("SecondUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FirstUser");

                    b.Navigation("SecondUser");
                });

            modelBuilder.Entity("PasswordManager.Models.Entities.Info", b =>
                {
                    b.HasOne("PasswordManager.Models.Entities.User", "User")
                        .WithMany("Infos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PasswordManager.Models.Entities.User", b =>
                {
                    b.Navigation("Infos");

                    b.Navigation("ReceivedFriendRequests");

                    b.Navigation("SentFriendRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
