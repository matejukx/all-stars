﻿// <auto-generated />
using System;
using AllStars.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AllStars.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AllStars.Domain.Dutch.Models.DutchGame", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("DutchGames");
                });

            modelBuilder.Entity("AllStars.Domain.Dutch.Models.DutchScore", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DutchGameId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uuid");

                    b.Property<int>("Points")
                        .HasColumnType("integer");

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DutchGameId");

                    b.HasIndex("PlayerId");

                    b.ToTable("DutchScores");
                });

            modelBuilder.Entity("AllStars.Domain.User.Models.AllStarUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Families")
                        .HasColumnType("integer");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AllStars.Domain.Dutch.Models.DutchScore", b =>
                {
                    b.HasOne("AllStars.Domain.Dutch.Models.DutchGame", "Game")
                        .WithMany("DutchScores")
                        .HasForeignKey("DutchGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AllStars.Domain.User.Models.AllStarUser", "Player")
                        .WithMany("DutchScores")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("AllStars.Domain.Dutch.Models.DutchGame", b =>
                {
                    b.Navigation("DutchScores");
                });

            modelBuilder.Entity("AllStars.Domain.User.Models.AllStarUser", b =>
                {
                    b.Navigation("DutchScores");
                });
#pragma warning restore 612, 618
        }
    }
}
