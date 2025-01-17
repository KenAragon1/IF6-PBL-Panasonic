﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace panasonic.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241219172709_AddRecoveryTokenColumnToUserTable")]
    partial class AddRecoveryTokenColumnToUserTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("panasonic.Models.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DetailMeasurement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DetailQuantity")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("UnitMeasurement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Number")
                        .IsUnique();

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("panasonic.Models.MaterialInventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductionLineId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("ProductionLineId");

                    b.ToTable("MaterialInventories", t =>
                        {
                            t.HasCheckConstraint("CK_MaterialInventory_MaterialInventoryLocation", "Location IN ('PreperationRoom', 'ProductionLine')");
                        });
                });

            modelBuilder.Entity("panasonic.Models.MaterialRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ApprovedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ApprovedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("FullfilledQuantity")
                        .HasColumnType("int");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<int>("ProductionLineId")
                        .HasColumnType("int");

                    b.Property<int?>("RejectedById")
                        .HasColumnType("int");

                    b.Property<int>("RequestedById")
                        .HasColumnType("int");

                    b.Property<int>("RequestedQuantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequiredAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Pending");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("VerifiedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApprovedById");

                    b.HasIndex("MaterialId");

                    b.HasIndex("ProductionLineId");

                    b.HasIndex("RejectedById");

                    b.HasIndex("RequestedById");

                    b.HasIndex("VerifiedById");

                    b.ToTable("MaterialRequests", t =>
                        {
                            t.HasCheckConstraint("CK_MaterialRequest_MaterialRequestStatus", "Status IN ('Pending', 'Verified', 'Approved', 'Rejected', 'Completed')");
                        });
                });

            modelBuilder.Entity("panasonic.Models.MaterialTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int?>("ProductionLineId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductionLineId");

                    b.HasIndex("UserId");

                    b.ToTable("MaterialTransactions", t =>
                        {
                            t.HasCheckConstraint("CK_MaterialTransaction_MaterialTransactionType", "Type IN ('Send', 'Production', 'Return', 'Pickup')");
                        });
                });

            modelBuilder.Entity("panasonic.Models.MaterialTransactionDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("TransactionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("TransactionId");

                    b.ToTable("MaterialTransactionDetails");
                });

            modelBuilder.Entity("panasonic.Models.ProductionLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Remark")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Remark")
                        .IsUnique();

                    b.ToTable("ProductionLines");
                });

            modelBuilder.Entity("panasonic.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("RecoveryToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Guest");

                    b.Property<DateTime?>("TokenExpiry")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("EmployeeID")
                        .IsUnique();

                    b.ToTable("Users", t =>
                        {
                            t.HasCheckConstraint("CK_User_UserRole", "Role IN ('ShiftLeader', 'AsistantLeader', 'StoreManager', 'Admin', 'MaterialHandler', 'Guest')");
                        });

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@email.com",
                            EmployeeID = 301010,
                            Fullname = "Admin",
                            HashedPassword = "AQAAAAIAAYagAAAAEJqTFzahQdx0zoclOvB+sPpRBV4ZvjClIjYriqCrdmR82aIjZ5r7xwbtv5F+6/t5Sg==",
                            IsDeleted = false,
                            IsVerified = true,
                            Role = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "asistantleader@email.com",
                            EmployeeID = 301011,
                            Fullname = "Asistant Leader",
                            HashedPassword = "AQAAAAIAAYagAAAAED6NoLZWW2akwcKkc4FSRsNPbIzdd8SmTfF6dsrn/+UGcqIcWwMjBB1dRUsZ4lcd8w==",
                            IsDeleted = false,
                            IsVerified = true,
                            Role = "AsistantLeader"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "shiftleader@email.com",
                            EmployeeID = 301012,
                            Fullname = "Shift Leader",
                            HashedPassword = "AQAAAAIAAYagAAAAEGOU1pptuZczwmJpqlj5+9jqUnkJXf9aCo4C7gwhQhwiJY8G8xZZpEejMnXwiVHa1A==",
                            IsDeleted = false,
                            IsVerified = true,
                            Role = "ShiftLeader"
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "storemanager@email.com",
                            EmployeeID = 301013,
                            Fullname = "Store Manager",
                            HashedPassword = "AQAAAAIAAYagAAAAEGND7jCj7xy7p9XoW08CoeJZoLlxtP8sivTN2CqWWbtTUfSUls42GktU8VPvWMRpaA==",
                            IsDeleted = false,
                            IsVerified = true,
                            Role = "StoreManager"
                        });
                });

            modelBuilder.Entity("panasonic.Models.MaterialInventory", b =>
                {
                    b.HasOne("panasonic.Models.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("panasonic.Models.ProductionLine", "ProductionLine")
                        .WithMany()
                        .HasForeignKey("ProductionLineId");

                    b.Navigation("Material");

                    b.Navigation("ProductionLine");
                });

            modelBuilder.Entity("panasonic.Models.MaterialRequest", b =>
                {
                    b.HasOne("panasonic.Models.User", "ApprovedBy")
                        .WithMany()
                        .HasForeignKey("ApprovedById");

                    b.HasOne("panasonic.Models.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("panasonic.Models.ProductionLine", "ProductionLine")
                        .WithMany()
                        .HasForeignKey("ProductionLineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("panasonic.Models.User", "RejectedBy")
                        .WithMany()
                        .HasForeignKey("RejectedById");

                    b.HasOne("panasonic.Models.User", "RequestedBy")
                        .WithMany()
                        .HasForeignKey("RequestedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("panasonic.Models.User", "VerifiedBy")
                        .WithMany()
                        .HasForeignKey("VerifiedById");

                    b.Navigation("ApprovedBy");

                    b.Navigation("Material");

                    b.Navigation("ProductionLine");

                    b.Navigation("RejectedBy");

                    b.Navigation("RequestedBy");

                    b.Navigation("VerifiedBy");
                });

            modelBuilder.Entity("panasonic.Models.MaterialTransaction", b =>
                {
                    b.HasOne("panasonic.Models.ProductionLine", "ProductionLine")
                        .WithMany()
                        .HasForeignKey("ProductionLineId");

                    b.HasOne("panasonic.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductionLine");

                    b.Navigation("User");
                });

            modelBuilder.Entity("panasonic.Models.MaterialTransactionDetail", b =>
                {
                    b.HasOne("panasonic.Models.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("panasonic.Models.MaterialTransaction", "MaterialTransaction")
                        .WithMany("MaterialTransactionDetails")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("MaterialTransaction");
                });

            modelBuilder.Entity("panasonic.Models.MaterialTransaction", b =>
                {
                    b.Navigation("MaterialTransactionDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
