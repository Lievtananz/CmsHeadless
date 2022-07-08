﻿// <auto-generated />
using System;
using CmsHeadless.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CmsHeadless.Migrations.CmsHeadlessDb
{
    [DbContext(typeof(CmsHeadlessDbContext))]
    [Migration("20220708095801_Add_Geocalocation_And_Typology")]
    partial class Add_Geocalocation_And_Typology
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CmsHeadless.Models.Attributes", b =>
                {
                    b.Property<int>("AttributesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttributesId"), 1L, 1);

                    b.Property<string>("AttributeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AttributeValue")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("AttributesId");

                    b.ToTable("Attributes");
                });

            modelBuilder.Entity("CmsHeadless.Models.AttributesGeolocation", b =>
                {
                    b.Property<int>("AttributesGeolocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttributesGeolocationId"), 1L, 1);

                    b.Property<int>("AttributesId")
                        .HasColumnType("int");

                    b.Property<int>("GeolocationId")
                        .HasColumnType("int");

                    b.HasKey("AttributesGeolocationId");

                    b.HasIndex("AttributesId");

                    b.HasIndex("GeolocationId");

                    b.ToTable("AttributesGeolocation");
                });

            modelBuilder.Entity("CmsHeadless.Models.AttributesTypology", b =>
                {
                    b.Property<int>("AttributesTypologyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttributesTypologyId"), 1L, 1);

                    b.Property<int>("AttributesId")
                        .HasColumnType("int");

                    b.Property<int>("TypologyId")
                        .HasColumnType("int");

                    b.HasKey("AttributesTypologyId");

                    b.HasIndex("AttributesId");

                    b.HasIndex("TypologyId");

                    b.ToTable("AttributesTypology");
                });

            modelBuilder.Entity("CmsHeadless.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<int?>("CategoryParentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Media")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("CategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("CmsHeadless.Models.Content", b =>
                {
                    b.Property<int>("ContentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContentId"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("InsertionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastEdit")
                        .HasColumnType("datetime2");

                    b.Property<string>("Media")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime?>("PubblicationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ContentId");

                    b.HasIndex("UserId");

                    b.ToTable("Content");
                });

            modelBuilder.Entity("CmsHeadless.Models.ContentAttributes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AttributesId")
                        .HasColumnType("int");

                    b.Property<int>("ContentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AttributesId");

                    b.HasIndex("ContentId");

                    b.ToTable("ContentAttributes");
                });

            modelBuilder.Entity("CmsHeadless.Models.ContentCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ContentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ContentId");

                    b.ToTable("ContentCategory");
                });

            modelBuilder.Entity("CmsHeadless.Models.ContentTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ContentId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContentId");

                    b.HasIndex("TagId");

                    b.ToTable("ContentTag");
                });

            modelBuilder.Entity("CmsHeadless.Models.Geolocation", b =>
                {
                    b.Property<int>("GeolocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GeolocationId"), 1L, 1);

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.HasKey("GeolocationId");

                    b.ToTable("Geolocation");
                });

            modelBuilder.Entity("CmsHeadless.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("TagId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("CmsHeadless.Models.Typology", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Typology");
                });

            modelBuilder.Entity("CmsHeadless.Models.UserGeolocation", b =>
                {
                    b.Property<int>("UserGeolocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserGeolocationId"), 1L, 1);

                    b.Property<int>("GeolocationId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserdId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserGeolocationId");

                    b.HasIndex("GeolocationId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGeolocation");
                });

            modelBuilder.Entity("CmsHeadless.Models.UserTypology", b =>
                {
                    b.Property<int>("UserTypologyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserTypologyId"), 1L, 1);

                    b.Property<int>("TypologyId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserTypologyId");

                    b.HasIndex("TypologyId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTypology");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("CmsHeadless.Models.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("LatitudeUser")
                        .HasColumnType("float");

                    b.Property<double>("LongitudeUser")
                        .HasColumnType("float");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ResidenceGeolocationId")
                        .HasColumnType("int");

                    b.HasIndex("ResidenceGeolocationId");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("CmsHeadless.Models.AttributesGeolocation", b =>
                {
                    b.HasOne("CmsHeadless.Models.Attributes", "Attributes")
                        .WithMany("AttributesGeolocation")
                        .HasForeignKey("AttributesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CmsHeadless.Models.Geolocation", "Geolocation")
                        .WithMany("AttributesGeolocation")
                        .HasForeignKey("GeolocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attributes");

                    b.Navigation("Geolocation");
                });

            modelBuilder.Entity("CmsHeadless.Models.AttributesTypology", b =>
                {
                    b.HasOne("CmsHeadless.Models.Attributes", "Attributes")
                        .WithMany("AttributesTypology")
                        .HasForeignKey("AttributesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CmsHeadless.Models.Typology", "Typology")
                        .WithMany("AttributesTypology")
                        .HasForeignKey("TypologyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attributes");

                    b.Navigation("Typology");
                });

            modelBuilder.Entity("CmsHeadless.Models.Content", b =>
                {
                    b.HasOne("CmsHeadless.Models.User", "User")
                        .WithMany("Content")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CmsHeadless.Models.ContentAttributes", b =>
                {
                    b.HasOne("CmsHeadless.Models.Attributes", "Attributes")
                        .WithMany("ContentAttributes")
                        .HasForeignKey("AttributesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CmsHeadless.Models.Content", "Content")
                        .WithMany("ContentAttributes")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attributes");

                    b.Navigation("Content");
                });

            modelBuilder.Entity("CmsHeadless.Models.ContentCategory", b =>
                {
                    b.HasOne("CmsHeadless.Models.Category", "Category")
                        .WithMany("ContentCategory")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CmsHeadless.Models.Content", "Content")
                        .WithMany("ContentCategory")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Content");
                });

            modelBuilder.Entity("CmsHeadless.Models.ContentTag", b =>
                {
                    b.HasOne("CmsHeadless.Models.Content", "Content")
                        .WithMany("ContentTag")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CmsHeadless.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Content");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("CmsHeadless.Models.UserGeolocation", b =>
                {
                    b.HasOne("CmsHeadless.Models.Geolocation", "Geolocation")
                        .WithMany("UserGeolocation")
                        .HasForeignKey("GeolocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CmsHeadless.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Geolocation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CmsHeadless.Models.UserTypology", b =>
                {
                    b.HasOne("CmsHeadless.Models.Typology", "Typology")
                        .WithMany("UserTypology")
                        .HasForeignKey("TypologyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CmsHeadless.Models.User", "User")
                        .WithMany("UserTypology")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Typology");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CmsHeadless.Models.User", b =>
                {
                    b.HasOne("CmsHeadless.Models.Geolocation", "Residence")
                        .WithMany()
                        .HasForeignKey("ResidenceGeolocationId");

                    b.Navigation("Residence");
                });

            modelBuilder.Entity("CmsHeadless.Models.Attributes", b =>
                {
                    b.Navigation("AttributesGeolocation");

                    b.Navigation("AttributesTypology");

                    b.Navigation("ContentAttributes");
                });

            modelBuilder.Entity("CmsHeadless.Models.Category", b =>
                {
                    b.Navigation("ContentCategory");
                });

            modelBuilder.Entity("CmsHeadless.Models.Content", b =>
                {
                    b.Navigation("ContentAttributes");

                    b.Navigation("ContentCategory");

                    b.Navigation("ContentTag");
                });

            modelBuilder.Entity("CmsHeadless.Models.Geolocation", b =>
                {
                    b.Navigation("AttributesGeolocation");

                    b.Navigation("UserGeolocation");
                });

            modelBuilder.Entity("CmsHeadless.Models.Typology", b =>
                {
                    b.Navigation("AttributesTypology");

                    b.Navigation("UserTypology");
                });

            modelBuilder.Entity("CmsHeadless.Models.User", b =>
                {
                    b.Navigation("Content");

                    b.Navigation("UserTypology");
                });
#pragma warning restore 612, 618
        }
    }
}
