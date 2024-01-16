﻿// <auto-generated />
using JwtPracticePart2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JwtPracticePart2.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JwtPracticePart2.Model.Entities.AccountEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("account_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UserRefId")
                        .HasColumnType("int")
                        .HasColumnName("User_id");

                    b.HasKey("Id");

                    b.HasIndex("UserRefId")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("JwtPracticePart2.Model.Entities.TokenEntity", b =>
                {
                    b.Property<int>("TokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("token_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TokenId"));

                    b.Property<int>("AccountRefId")
                        .HasColumnType("int");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("refresh_token");

                    b.HasKey("TokenId");

                    b.HasIndex("AccountRefId")
                        .IsUnique();

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("JwtPracticePart2.Model.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("JwtPracticePart2.Model.Entities.AccountEntity", b =>
                {
                    b.HasOne("JwtPracticePart2.Model.Entities.UserEntity", "User")
                        .WithOne("Account")
                        .HasForeignKey("JwtPracticePart2.Model.Entities.AccountEntity", "UserRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("JwtPracticePart2.Model.Entities.TokenEntity", b =>
                {
                    b.HasOne("JwtPracticePart2.Model.Entities.AccountEntity", "Account")
                        .WithOne("Token")
                        .HasForeignKey("JwtPracticePart2.Model.Entities.TokenEntity", "AccountRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("JwtPracticePart2.Model.Entities.AccountEntity", b =>
                {
                    b.Navigation("Token")
                        .IsRequired();
                });

            modelBuilder.Entity("JwtPracticePart2.Model.Entities.UserEntity", b =>
                {
                    b.Navigation("Account")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
