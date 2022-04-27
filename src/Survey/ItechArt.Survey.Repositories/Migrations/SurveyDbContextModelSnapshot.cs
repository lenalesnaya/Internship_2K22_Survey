﻿// <auto-generated />
using System;
using ItechArt.Survey.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ItechArt.Survey.Repositories.Migrations
{
    [DbContext(typeof(SurveyDbContext))]
    partial class SurveyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ItechArt.Survey.DomainModel.SurveyModel.Answers.AnswerVariant", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("QuestionId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("AnswerVariant", (string)null);
                });

            modelBuilder.Entity("ItechArt.Survey.DomainModel.SurveyModel.Questions.AnswerVariantsQuestion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<bool>("CanChooseManyAnswers")
                        .HasColumnType("bit");

                    b.Property<long>("SurveyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("AnswerVariantsQuestion", (string)null);
                });

            modelBuilder.Entity("ItechArt.Survey.DomainModel.SurveyModel.Questions.FileAnswerQuestion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("SurveyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("FileAnswerQuestion", (string)null);
                });

            modelBuilder.Entity("ItechArt.Survey.DomainModel.SurveyModel.Questions.ScaleAnswerQuestion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<int>("ScaleMaxValue")
                        .HasColumnType("int");

                    b.Property<int>("ScaleMinValue")
                        .HasColumnType("int");

                    b.Property<long>("SurveyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("ScaleAnswerQuestion", (string)null);
                });

            modelBuilder.Entity("ItechArt.Survey.DomainModel.SurveyModel.Questions.StarRatingAnswerQuestion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<int>("AmountOfStars")
                        .HasColumnType("int");

                    b.Property<long>("SurveyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("StarRatingAnswerQuestion", (string)null);
                });

            modelBuilder.Entity("ItechArt.Survey.DomainModel.SurveyModel.Questions.TextAnswerQuestion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("SurveyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("TextAnswerQuestion", (string)null);
                });

            modelBuilder.Entity("ItechArt.Survey.DomainModel.SurveyModel.Survey", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAnonymous")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Survey", (string)null);
                });

            modelBuilder.Entity("ItechArt.Survey.DomainModel.UserModel.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("NormalizedName")
                        .IsUnique();

                    b.ToTable("Role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcurrencyStamp = "7612cd22-c0f0-4801-a3e5-ff7cd1a41302",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = 2,
                            ConcurrencyStamp = "7612cd22-c0f0-4801-a3e5-ff7cd1a41301",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        });
                });

            modelBuilder.Entity("ItechArt.Survey.DomainModel.UserModel.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("AvatarFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("NormalizedUserName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("NormalizedEmail")
                        .IsUnique();

                    b.HasIndex("NormalizedUserName")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = -1,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "85263788-277f-4f89-b8c4-a11ac465ed58",
                            Email = "admin@mail.ru",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@MAIL.RU",
                            NormalizedUserName = "ADMINISTRATOR",
                            PasswordHash = "AQAAAAEAACcQAAAAEA8M7189kTA4Dmjh3f/OWj6KLgYSUI4EelUsFdP0k7XOAbuUw7hQm15H2qz3aSTIpw==",
                            PhoneNumberConfirmed = false,
                            RegistrationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TwoFactorEnabled = false,
                            UserName = "Administrator"
                        });
                });

            modelBuilder.Entity("ItechArt.Survey.DomainModel.UserModel.UserRole", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole", (string)null);

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            UserId = -1
                        },
                        new
                        {
                            RoleId = 2,
                            UserId = -1
                        });
                });

            modelBuilder.Entity("ItechArt.Survey.DomainModel.SurveyModel.Answers.AnswerVariant", b =>
                {
                    b.HasOne("ItechArt.Survey.DomainModel.SurveyModel.Questions.AnswerVariantsQuestion", "Question")
                        .WithMany("AnswerVariants")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("ItechArt.Survey.DomainModel.SurveyModel.Questions.AnswerVariantsQuestion", b =>
                {
                    b.HasOne("ItechArt.Survey.DomainModel.SurveyModel.Survey", "Survey")
                        .WithMany("AnswerVariantsQuestions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("ItechArt.Survey.DomainModel.SurveyModel.Questions.FileAnswerQuestion", b =>
                {
                    b.HasOne("ItechArt.Survey.DomainModel.SurveyModel.Survey", "Survey")
                        .WithMany("FileAnswerQuestions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("ItechArt.Survey.DomainModel.SurveyModel.Questions.ScaleAnswerQuestion", b =>
                {
                    b.HasOne("ItechArt.Survey.DomainModel.SurveyModel.Survey", "Survey")
                        .WithMany("ScaleAnswerQuestions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("ItechArt.Survey.DomainModel.SurveyModel.Questions.StarRatingAnswerQuestion", b =>
                {
                    b.HasOne("ItechArt.Survey.DomainModel.SurveyModel.Survey", "Survey")
                        .WithMany("StarRatingAnswerQuestions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("ItechArt.Survey.DomainModel.SurveyModel.Questions.TextAnswerQuestion", b =>
                {
                    b.HasOne("ItechArt.Survey.DomainModel.SurveyModel.Survey", "Survey")
                        .WithMany("TextAnswerQuestions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("ItechArt.Survey.DomainModel.SurveyModel.Survey", b =>
                {
                    b.HasOne("ItechArt.Survey.DomainModel.UserModel.User", "Сreator")
                        .WithMany("Surveys")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Сreator");
                });

            modelBuilder.Entity("ItechArt.Survey.DomainModel.UserModel.UserRole", b =>
                {
                    b.HasOne("ItechArt.Survey.DomainModel.UserModel.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ItechArt.Survey.DomainModel.UserModel.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ItechArt.Survey.DomainModel.SurveyModel.Questions.AnswerVariantsQuestion", b =>
                {
                    b.Navigation("AnswerVariants");
                });

            modelBuilder.Entity("ItechArt.Survey.DomainModel.SurveyModel.Survey", b =>
                {
                    b.Navigation("AnswerVariantsQuestions");

                    b.Navigation("FileAnswerQuestions");

                    b.Navigation("ScaleAnswerQuestions");

                    b.Navigation("StarRatingAnswerQuestions");

                    b.Navigation("TextAnswerQuestions");
                });

            modelBuilder.Entity("ItechArt.Survey.DomainModel.UserModel.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("ItechArt.Survey.DomainModel.UserModel.User", b =>
                {
                    b.Navigation("Surveys");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
