﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("Domain.Entities.BusinessParticipant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Info")
                        .HasMaxLength(5000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ParticipantsNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("BusinessParticipants");
                });

            modelBuilder.Entity("Domain.Entities.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Info")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Domain.Entities.EventParticipant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("BusinessParticipantId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EventId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PrivateParticipantId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BusinessParticipantId");

                    b.HasIndex("EventId");

                    b.HasIndex("PrivateParticipantId");

                    b.ToTable("EventParticipants");
                });

            modelBuilder.Entity("Domain.Entities.PrivateParticipant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Info")
                        .HasMaxLength(1500)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("PrivateParticipants");
                });

            modelBuilder.Entity("Domain.Entities.EventParticipant", b =>
                {
                    b.HasOne("Domain.Entities.BusinessParticipant", "BusinessParticipant")
                        .WithMany("EventParticipants")
                        .HasForeignKey("BusinessParticipantId");

                    b.HasOne("Domain.Entities.Event", "Event")
                        .WithMany("EventParticipants")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.PrivateParticipant", "PrivateParticipant")
                        .WithMany("EventParticipants")
                        .HasForeignKey("PrivateParticipantId");

                    b.Navigation("BusinessParticipant");

                    b.Navigation("Event");

                    b.Navigation("PrivateParticipant");
                });

            modelBuilder.Entity("Domain.Entities.BusinessParticipant", b =>
                {
                    b.Navigation("EventParticipants");
                });

            modelBuilder.Entity("Domain.Entities.Event", b =>
                {
                    b.Navigation("EventParticipants");
                });

            modelBuilder.Entity("Domain.Entities.PrivateParticipant", b =>
                {
                    b.Navigation("EventParticipants");
                });
#pragma warning restore 612, 618
        }
    }
}
