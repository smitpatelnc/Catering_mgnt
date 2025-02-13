﻿// <auto-generated />
using System;
using CateringManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CateringManagement.Data.CMMigrations
{
    [DbContext(typeof(CateringContext))]
    [Migration("20231206180537_Email")]
    partial class Email
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("CateringManagement.Models.Customer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CompanyName")
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("CustomerCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("CustomerCode")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CateringManagement.Models.CustomerPhoto", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Content")
                        .HasColumnType("BLOB");

                    b.Property<int>("CustomerID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MimeType")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("CustomerID")
                        .IsUnique();

                    b.ToTable("CustomerPhotos");
                });

            modelBuilder.Entity("CateringManagement.Models.CustomerThumbnail", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Content")
                        .HasColumnType("BLOB");

                    b.Property<int>("CustomerID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MimeType")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("CustomerID")
                        .IsUnique();

                    b.ToTable("CustomerThumbnails");
                });

            modelBuilder.Entity("CateringManagement.Models.Equipment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("TEXT");

                    b.Property<double>("StandardCharge")
                        .HasColumnType("REAL");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Equipments");
                });

            modelBuilder.Entity("CateringManagement.Models.FileContent", b =>
                {
                    b.Property<int>("FileContentID")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Content")
                        .HasColumnType("BLOB");

                    b.HasKey("FileContentID");

                    b.ToTable("FileContent");
                });

            modelBuilder.Entity("CateringManagement.Models.Function", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Alcohol")
                        .HasColumnType("INTEGER");

                    b.Property<double>("BaseCharge")
                        .HasColumnType("REAL");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("CustomerID")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Deposit")
                        .HasColumnType("REAL");

                    b.Property<bool>("DepositPaid")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("FunctionTypeID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GuaranteedNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LobbySign")
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.Property<int?>("MealTypeID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.Property<bool>("NoGratuity")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("NoHST")
                        .HasColumnType("INTEGER");

                    b.Property<double>("PerPersonCharge")
                        .HasColumnType("REAL");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.Property<double>("SOCAN")
                        .HasColumnType("REAL");

                    b.Property<string>("SetupNotes")
                        .HasMaxLength(2000)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("FunctionTypeID");

                    b.HasIndex("MealTypeID");

                    b.ToTable("Functions");
                });

            modelBuilder.Entity("CateringManagement.Models.FunctionEquipment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EquipmentID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FunctionID")
                        .HasColumnType("INTEGER");

                    b.Property<double>("PerUnitCharge")
                        .HasColumnType("REAL");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("FunctionID");

                    b.HasIndex("EquipmentID", "FunctionID")
                        .IsUnique();

                    b.ToTable("FunctionEquipments");
                });

            modelBuilder.Entity("CateringManagement.Models.FunctionRoom", b =>
                {
                    b.Property<int>("FunctionID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoomID")
                        .HasColumnType("INTEGER");

                    b.HasKey("FunctionID", "RoomID");

                    b.HasIndex("RoomID");

                    b.ToTable("FunctionRooms");
                });

            modelBuilder.Entity("CateringManagement.Models.FunctionType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("FunctionTypes");
                });

            modelBuilder.Entity("CateringManagement.Models.MealType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("MealTypes");
                });

            modelBuilder.Entity("CateringManagement.Models.Room", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Capacity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("CateringManagement.Models.UploadedFile", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("MimeType")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("UploadedFiles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("UploadedFile");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("CateringManagement.Models.Work", b =>
                {
                    b.Property<int>("FunctionID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WorkerID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Points")
                        .HasColumnType("INTEGER");

                    b.HasKey("FunctionID", "WorkerID");

                    b.HasIndex("WorkerID");

                    b.ToTable("Works");
                });

            modelBuilder.Entity("CateringManagement.Models.Worker", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("CateringManagement.ViewModels.FunctionRevenueVM", b =>
                {
                    b.Property<int>("ID")
                        .HasColumnType("INTEGER");

                    b.Property<double>("AverageGuarNo")
                        .HasColumnType("REAL");

                    b.Property<double>("AveragePPCharge")
                        .HasColumnType("REAL");

                    b.Property<double>("AvgValue")
                        .HasColumnType("REAL");

                    b.Property<double>("MaxValue")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("TotalNumber")
                        .HasColumnType("INTEGER");

                    b.Property<double>("TotalValue")
                        .HasColumnType("REAL");

                    b.HasKey("ID");

                    b.ToTable((string)null);

                    b.ToView("FunctionRevenueSummary", (string)null);
                });

            modelBuilder.Entity("CateringManagement.Models.FunctionDocument", b =>
                {
                    b.HasBaseType("CateringManagement.Models.UploadedFile");

                    b.Property<int>("FunctionID")
                        .HasColumnType("INTEGER");

                    b.HasIndex("FunctionID");

                    b.HasDiscriminator().HasValue("FunctionDocument");
                });

            modelBuilder.Entity("CateringManagement.Models.CustomerPhoto", b =>
                {
                    b.HasOne("CateringManagement.Models.Customer", "Customer")
                        .WithOne("CustomerPhoto")
                        .HasForeignKey("CateringManagement.Models.CustomerPhoto", "CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("CateringManagement.Models.CustomerThumbnail", b =>
                {
                    b.HasOne("CateringManagement.Models.Customer", "Customer")
                        .WithOne("CustomerThumbnail")
                        .HasForeignKey("CateringManagement.Models.CustomerThumbnail", "CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("CateringManagement.Models.FileContent", b =>
                {
                    b.HasOne("CateringManagement.Models.UploadedFile", "UploadedFile")
                        .WithOne("FileContent")
                        .HasForeignKey("CateringManagement.Models.FileContent", "FileContentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UploadedFile");
                });

            modelBuilder.Entity("CateringManagement.Models.Function", b =>
                {
                    b.HasOne("CateringManagement.Models.Customer", "Customer")
                        .WithMany("Functions")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CateringManagement.Models.FunctionType", "FunctionType")
                        .WithMany("Functions")
                        .HasForeignKey("FunctionTypeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CateringManagement.Models.MealType", "MealType")
                        .WithMany("Functions")
                        .HasForeignKey("MealTypeID");

                    b.Navigation("Customer");

                    b.Navigation("FunctionType");

                    b.Navigation("MealType");
                });

            modelBuilder.Entity("CateringManagement.Models.FunctionEquipment", b =>
                {
                    b.HasOne("CateringManagement.Models.Equipment", "Equipment")
                        .WithMany("FunctionEquipments")
                        .HasForeignKey("EquipmentID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CateringManagement.Models.Function", "Function")
                        .WithMany("FunctionEquipments")
                        .HasForeignKey("FunctionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipment");

                    b.Navigation("Function");
                });

            modelBuilder.Entity("CateringManagement.Models.FunctionRoom", b =>
                {
                    b.HasOne("CateringManagement.Models.Function", "Function")
                        .WithMany("FunctionRooms")
                        .HasForeignKey("FunctionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CateringManagement.Models.Room", "Room")
                        .WithMany("FunctionRooms")
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Function");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("CateringManagement.Models.Work", b =>
                {
                    b.HasOne("CateringManagement.Models.Function", "Function")
                        .WithMany("Works")
                        .HasForeignKey("FunctionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CateringManagement.Models.Worker", "Worker")
                        .WithMany("Works")
                        .HasForeignKey("WorkerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Function");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("CateringManagement.Models.FunctionDocument", b =>
                {
                    b.HasOne("CateringManagement.Models.Function", "Function")
                        .WithMany("FunctionDocuments")
                        .HasForeignKey("FunctionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Function");
                });

            modelBuilder.Entity("CateringManagement.Models.Customer", b =>
                {
                    b.Navigation("CustomerPhoto");

                    b.Navigation("CustomerThumbnail");

                    b.Navigation("Functions");
                });

            modelBuilder.Entity("CateringManagement.Models.Equipment", b =>
                {
                    b.Navigation("FunctionEquipments");
                });

            modelBuilder.Entity("CateringManagement.Models.Function", b =>
                {
                    b.Navigation("FunctionDocuments");

                    b.Navigation("FunctionEquipments");

                    b.Navigation("FunctionRooms");

                    b.Navigation("Works");
                });

            modelBuilder.Entity("CateringManagement.Models.FunctionType", b =>
                {
                    b.Navigation("Functions");
                });

            modelBuilder.Entity("CateringManagement.Models.MealType", b =>
                {
                    b.Navigation("Functions");
                });

            modelBuilder.Entity("CateringManagement.Models.Room", b =>
                {
                    b.Navigation("FunctionRooms");
                });

            modelBuilder.Entity("CateringManagement.Models.UploadedFile", b =>
                {
                    b.Navigation("FileContent");
                });

            modelBuilder.Entity("CateringManagement.Models.Worker", b =>
                {
                    b.Navigation("Works");
                });
#pragma warning restore 612, 618
        }
    }
}
