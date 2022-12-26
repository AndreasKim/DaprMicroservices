﻿//// <auto-generated />
//using System;
//using Core.Infrastructure.Persistence;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.EntityFrameworkCore.Migrations;
//using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

//#nullable disable

//namespace Infrastructure.Migrations
//{
//    [DbContext(typeof(ApplicationDbContext))]
//    [Migration("20221204130519_Initial2")]
//    partial class Initial2
//    {
//        protected override void BuildTargetModel(ModelBuilder modelBuilder)
//        {
//#pragma warning disable 612, 618
//            modelBuilder
//                .HasAnnotation("ProductVersion", "6.0.7")
//                .HasAnnotation("Relational:MaxIdentifierLength", 128);

//            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

//            modelBuilder.Entity("Core.Domain.Entities.Conversation", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

//                    b.Property<string>("BuyerId")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<int>("ProductId")
//                        .HasColumnType("int");

//                    b.Property<long?>("ProductId1")
//                        .HasColumnType("bigint");

//                    b.Property<string>("SellerId")
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("Id");

//                    b.HasIndex("ProductId1");

//                    b.ToTable("Conversations");
//                });

//            modelBuilder.Entity("Core.Domain.Entities.Message", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

//                    b.Property<string>("Content")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<int>("ConversationId")
//                        .HasColumnType("int");

//                    b.Property<string>("FromUserId")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<DateTime>("Time")
//                        .HasColumnType("datetime2");

//                    b.Property<string>("ToUserId")
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("Id");

//                    b.HasIndex("ConversationId");

//                    b.ToTable("Message");
//                });

//            modelBuilder.Entity("Core.Domain.Entities.Product", b =>
//                {
//                    b.Property<long>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("bigint");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

//                    b.Property<DateTime>("Created")
//                        .HasColumnType("datetime2");

//                    b.Property<string>("Description")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<bool>("Individualized")
//                        .HasColumnType("bit");

//                    b.Property<int>("MainCategory")
//                        .HasColumnType("int");

//                    b.Property<string>("Name")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<double>("Price")
//                        .HasColumnType("float");

//                    b.Property<int>("SalesInfoId")
//                        .HasColumnType("int");

//                    b.Property<string>("SubCategory")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Thumbnail")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<int>("VendorId")
//                        .HasColumnType("int");

//                    b.Property<string>("VendorName")
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("Id");

//                    b.HasIndex("SalesInfoId");

//                    b.HasIndex("VendorId");

//                    b.ToTable("Products");
//                });

//            modelBuilder.Entity("Core.Domain.Entities.Rating", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

//                    b.Property<string>("Description")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<double>("RatingValue")
//                        .HasColumnType("float");

//                    b.Property<int>("SalesInfoId")
//                        .HasColumnType("int");

//                    b.Property<string>("UserFullName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("UserId")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("Id");

//                    b.HasIndex("SalesInfoId");

//                    b.ToTable("Ratings");
//                });

//            modelBuilder.Entity("Core.Domain.Entities.SalesInfo", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

//                    b.Property<int>("NumberOfSales")
//                        .HasColumnType("int");

//                    b.Property<string>("RatingScore")
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("Id");

//                    b.ToTable("SalesInfos");
//                });

//            modelBuilder.Entity("Core.Domain.Entities.Vendor", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

//                    b.Property<string>("AGB")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("City")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("CorporateEmail")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Description")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Logo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Name")
//                        .IsRequired()
//                        .HasMaxLength(50)
//                        .HasColumnType("nvarchar(50)");

//                    b.Property<int>("NumberOfSales")
//                        .HasColumnType("int");

//                    b.Property<string>("PLZ")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<double>("Rating")
//                        .HasMaxLength(1)
//                        .HasColumnType("float");

//                    b.Property<string>("Revocation")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Shipment")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("ShopOwnerId")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Street")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("StreetNo")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Telephone")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<double>("TotalAmountSales")
//                        .HasColumnType("float");

//                    b.HasKey("Id");

//                    b.ToTable("Vendors");
//                });

//            modelBuilder.Entity("Core.Infrastructure.Identity.Address", b =>
//                {
//                    b.Property<long>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("bigint");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

//                    b.Property<string>("City")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("CorporateEmail")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("PLZ")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Street")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("StreetNo")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Telephone")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("Id");

//                    b.ToTable("Address");
//                });

//            modelBuilder.Entity("Core.Infrastructure.Identity.BBUser", b =>
//                {
//                    b.Property<string>("Id")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<int>("AccessFailedCount")
//                        .HasColumnType("int");

//                    b.Property<long?>("AddressId")
//                        .HasColumnType("bigint");

//                    b.Property<string>("ConcurrencyStamp")
//                        .IsConcurrencyToken()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Email")
//                        .HasMaxLength(256)
//                        .HasColumnType("nvarchar(256)");

//                    b.Property<bool>("EmailConfirmed")
//                        .HasColumnType("bit");

//                    b.Property<string>("FirstName")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("LastName")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<bool>("LockoutEnabled")
//                        .HasColumnType("bit");

//                    b.Property<DateTimeOffset?>("LockoutEnd")
//                        .HasColumnType("datetimeoffset");

//                    b.Property<string>("NormalizedEmail")
//                        .HasMaxLength(256)
//                        .HasColumnType("nvarchar(256)");

//                    b.Property<string>("NormalizedUserName")
//                        .HasMaxLength(256)
//                        .HasColumnType("nvarchar(256)");

//                    b.Property<string>("PasswordHash")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<long?>("PaymentDetailsId")
//                        .HasColumnType("bigint");

//                    b.Property<string>("PhoneNumber")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<bool>("PhoneNumberConfirmed")
//                        .HasColumnType("bit");

//                    b.Property<string>("SecurityStamp")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<bool>("TwoFactorEnabled")
//                        .HasColumnType("bit");

//                    b.Property<string>("UserName")
//                        .HasMaxLength(256)
//                        .HasColumnType("nvarchar(256)");

//                    b.HasKey("Id");

//                    b.HasIndex("AddressId");

//                    b.HasIndex("NormalizedEmail")
//                        .HasDatabaseName("EmailIndex");

//                    b.HasIndex("NormalizedUserName")
//                        .IsUnique()
//                        .HasDatabaseName("UserNameIndex")
//                        .HasFilter("[NormalizedUserName] IS NOT NULL");

//                    b.HasIndex("PaymentDetailsId");

//                    b.ToTable("AspNetUsers", (string)null);
//                });

//            modelBuilder.Entity("Core.Infrastructure.Identity.PaymentDetails", b =>
//                {
//                    b.Property<long>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("bigint");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

//                    b.Property<string>("CVV")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("CardHolderName")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("CreditCardNumber")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<DateTime>("ValidTill")
//                        .HasColumnType("datetime2");

//                    b.HasKey("Id");

//                    b.ToTable("PaymentDetails");
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
//                {
//                    b.Property<string>("Id")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<string>("ConcurrencyStamp")
//                        .IsConcurrencyToken()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Name")
//                        .HasMaxLength(256)
//                        .HasColumnType("nvarchar(256)");

//                    b.Property<string>("NormalizedName")
//                        .HasMaxLength(256)
//                        .HasColumnType("nvarchar(256)");

//                    b.HasKey("Id");

//                    b.HasIndex("NormalizedName")
//                        .IsUnique()
//                        .HasDatabaseName("RoleNameIndex")
//                        .HasFilter("[NormalizedName] IS NOT NULL");

//                    b.ToTable("AspNetRoles", (string)null);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

//                    b.Property<string>("ClaimType")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("ClaimValue")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("RoleId")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(450)");

//                    b.HasKey("Id");

//                    b.HasIndex("RoleId");

//                    b.ToTable("AspNetRoleClaims", (string)null);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

//                    b.Property<string>("ClaimType")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("ClaimValue")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("UserId")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(450)");

//                    b.HasKey("Id");

//                    b.HasIndex("UserId");

//                    b.ToTable("AspNetUserClaims", (string)null);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
//                {
//                    b.Property<string>("LoginProvider")
//                        .HasMaxLength(128)
//                        .HasColumnType("nvarchar(128)");

//                    b.Property<string>("ProviderKey")
//                        .HasMaxLength(128)
//                        .HasColumnType("nvarchar(128)");

//                    b.Property<string>("ProviderDisplayName")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("UserId")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(450)");

//                    b.HasKey("LoginProvider", "ProviderKey");

//                    b.HasIndex("UserId");

//                    b.ToTable("AspNetUserLogins", (string)null);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
//                {
//                    b.Property<string>("UserId")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<string>("RoleId")
//                        .HasColumnType("nvarchar(450)");

//                    b.HasKey("UserId", "RoleId");

//                    b.HasIndex("RoleId");

//                    b.ToTable("AspNetUserRoles", (string)null);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
//                {
//                    b.Property<string>("UserId")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<string>("LoginProvider")
//                        .HasMaxLength(128)
//                        .HasColumnType("nvarchar(128)");

//                    b.Property<string>("Name")
//                        .HasMaxLength(128)
//                        .HasColumnType("nvarchar(128)");

//                    b.Property<string>("Value")
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("UserId", "LoginProvider", "Name");

//                    b.ToTable("AspNetUserTokens", (string)null);
//                });

//            modelBuilder.Entity("Core.Domain.Entities.Conversation", b =>
//                {
//                    b.HasOne("Core.Domain.Entities.Product", "Product")
//                        .WithMany()
//                        .HasForeignKey("ProductId1");

//                    b.Navigation("Product");
//                });

//            modelBuilder.Entity("Core.Domain.Entities.Message", b =>
//                {
//                    b.HasOne("Core.Domain.Entities.Conversation", null)
//                        .WithMany("Messages")
//                        .HasForeignKey("ConversationId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();
//                });

//            modelBuilder.Entity("Core.Domain.Entities.Product", b =>
//                {
//                    b.HasOne("Core.Domain.Entities.SalesInfo", "SalesInfo")
//                        .WithMany()
//                        .HasForeignKey("SalesInfoId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.HasOne("Core.Domain.Entities.Vendor", "Vendor")
//                        .WithMany("Products")
//                        .HasForeignKey("VendorId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.Navigation("SalesInfo");

//                    b.Navigation("Vendor");
//                });

//            modelBuilder.Entity("Core.Domain.Entities.Rating", b =>
//                {
//                    b.HasOne("Core.Domain.Entities.SalesInfo", null)
//                        .WithMany("Ratings")
//                        .HasForeignKey("SalesInfoId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();
//                });

//            modelBuilder.Entity("Core.Infrastructure.Identity.BBUser", b =>
//                {
//                    b.HasOne("Core.Infrastructure.Identity.Address", "Address")
//                        .WithMany()
//                        .HasForeignKey("AddressId");

//                    b.HasOne("Core.Infrastructure.Identity.PaymentDetails", "PaymentDetails")
//                        .WithMany()
//                        .HasForeignKey("PaymentDetailsId");

//                    b.Navigation("Address");

//                    b.Navigation("PaymentDetails");
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
//                {
//                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
//                        .WithMany()
//                        .HasForeignKey("RoleId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
//                {
//                    b.HasOne("Core.Infrastructure.Identity.BBUser", null)
//                        .WithMany()
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
//                {
//                    b.HasOne("Core.Infrastructure.Identity.BBUser", null)
//                        .WithMany()
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
//                {
//                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
//                        .WithMany()
//                        .HasForeignKey("RoleId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.HasOne("Core.Infrastructure.Identity.BBUser", null)
//                        .WithMany()
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
//                {
//                    b.HasOne("Core.Infrastructure.Identity.BBUser", null)
//                        .WithMany()
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();
//                });

//            modelBuilder.Entity("Core.Domain.Entities.Conversation", b =>
//                {
//                    b.Navigation("Messages");
//                });

//            modelBuilder.Entity("Core.Domain.Entities.SalesInfo", b =>
//                {
//                    b.Navigation("Ratings");
//                });

//            modelBuilder.Entity("Core.Domain.Entities.Vendor", b =>
//                {
//                    b.Navigation("Products");
//                });
//#pragma warning restore 612, 618
//        }
//    }
//}
