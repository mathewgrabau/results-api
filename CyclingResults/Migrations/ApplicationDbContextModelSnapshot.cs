// <auto-generated />
using System;
using CyclingResults.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CyclingResults.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("CyclingResults.Domain.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<double?>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double?>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("CyclingResults.Domain.Race", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Category")
                        .HasColumnType("TEXT");

                    b.Property<string>("Classification")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("EventId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Laps")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("RaceType")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Races");
                });

            modelBuilder.Entity("CyclingResults.Domain.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<int>("LapsCompleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("LicenseVerified")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Place")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PlateNumber")
                        .HasColumnType("TEXT");

                    b.Property<int>("RaceId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("ResultTime")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Started")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TeamName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RaceId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("CyclingResults.Domain.ResultUpload", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("RaceId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("ResultId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RaceId");

                    b.ToTable("ResultUploads");
                });

            modelBuilder.Entity("CyclingResults.Domain.Race", b =>
                {
                    b.HasOne("CyclingResults.Domain.Event", "Event")
                        .WithMany("Races")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("CyclingResults.Domain.Result", b =>
                {
                    b.HasOne("CyclingResults.Domain.Race", "Race")
                        .WithMany("Results")
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Race");
                });

            modelBuilder.Entity("CyclingResults.Domain.ResultUpload", b =>
                {
                    b.HasOne("CyclingResults.Domain.Race", "Race")
                        .WithMany()
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Race");
                });

            modelBuilder.Entity("CyclingResults.Domain.Event", b =>
                {
                    b.Navigation("Races");
                });

            modelBuilder.Entity("CyclingResults.Domain.Race", b =>
                {
                    b.Navigation("Results");
                });
#pragma warning restore 612, 618
        }
    }
}
