﻿// <auto-generated />
using System;
using Licenta.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Licenta.Migrations
{
    [DbContext(typeof(ContextDb))]
    [Migration("20220613092619_DeletePhoto")]
    partial class DeletePhoto
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Licenta.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Licenta.Models.Meeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("FacilitatorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("Licenta.Models.MemberMeeting", b =>
                {
                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("MeetingId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.HasKey("MemberId", "MeetingId");

                    b.HasIndex("MeetingId");

                    b.ToTable("MemberMeetings");
                });

            modelBuilder.Entity("Licenta.Models.MemberModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Statut")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("StatutChangeDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TelNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("University")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Licenta.Models.MemberTraining", b =>
                {
                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("TrainingId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.HasKey("MemberId", "TrainingId");

                    b.HasIndex("TrainingId");

                    b.ToTable("MemberTrainings");
                });

            modelBuilder.Entity("Licenta.Models.Responsibility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ResponsibleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("ResponsibleId");

                    b.ToTable("Responsibilities");
                });

            modelBuilder.Entity("Licenta.Models.Training", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TrainerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Trainings");
                });

            modelBuilder.Entity("Licenta.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Licenta.Models.MemberMeeting", b =>
                {
                    b.HasOne("Licenta.Models.Meeting", "Meeting")
                        .WithMany("Participants")
                        .HasForeignKey("MeetingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Licenta.Models.MemberModel", "Member")
                        .WithMany("MemberMeetings")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meeting");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("Licenta.Models.MemberModel", b =>
                {
                    b.HasOne("Licenta.Models.User", "User")
                        .WithOne("Member")
                        .HasForeignKey("Licenta.Models.MemberModel", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Licenta.Models.MemberTraining", b =>
                {
                    b.HasOne("Licenta.Models.MemberModel", "Member")
                        .WithMany("MemberTrainings")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Licenta.Models.Training", "Training")
                        .WithMany("Participants")
                        .HasForeignKey("TrainingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Training");
                });

            modelBuilder.Entity("Licenta.Models.Responsibility", b =>
                {
                    b.HasOne("Licenta.Models.Event", "Event")
                        .WithMany("Responsibilities")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Licenta.Models.MemberModel", "Responsible")
                        .WithMany("Responsibilities")
                        .HasForeignKey("ResponsibleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Responsible");
                });

            modelBuilder.Entity("Licenta.Models.Event", b =>
                {
                    b.Navigation("Responsibilities");
                });

            modelBuilder.Entity("Licenta.Models.Meeting", b =>
                {
                    b.Navigation("Participants");
                });

            modelBuilder.Entity("Licenta.Models.MemberModel", b =>
                {
                    b.Navigation("MemberMeetings");

                    b.Navigation("MemberTrainings");

                    b.Navigation("Responsibilities");
                });

            modelBuilder.Entity("Licenta.Models.Training", b =>
                {
                    b.Navigation("Participants");
                });

            modelBuilder.Entity("Licenta.Models.User", b =>
                {
                    b.Navigation("Member");
                });
#pragma warning restore 612, 618
        }
    }
}
