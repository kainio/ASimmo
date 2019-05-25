﻿// <auto-generated />
using System;
using ASimmo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ASimmo.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity("ASimmo.Models.Adresse", b =>
                {
                    b.Property<int>("AdresseId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdressePostale");

                    b.Property<string>("CodePostal");

                    b.Property<float>("Lat");

                    b.Property<float>("Lon");

                    b.Property<int>("PromoteurId");

                    b.Property<string>("Quartier");

                    b.Property<string>("Ville");

                    b.HasKey("AdresseId");

                    b.HasIndex("PromoteurId");

                    b.ToTable("Adresses");
                });

            modelBuilder.Entity("ASimmo.Models.BienImmo", b =>
                {
                    b.Property<int>("BienImmoId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdresseId");

                    b.Property<int>("ClassificationId");

                    b.Property<string>("Image");

                    b.Property<int>("NombreChambre");

                    b.Property<decimal>("Prix");

                    b.Property<decimal>("Surface");

                    b.Property<int>("TypeId");

                    b.HasKey("BienImmoId");

                    b.HasIndex("AdresseId");

                    b.HasIndex("ClassificationId");

                    b.HasIndex("TypeId");

                    b.ToTable("BiensImmos");
                });

            modelBuilder.Entity("ASimmo.Models.Classification", b =>
                {
                    b.Property<int>("ClassificationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Image");

                    b.Property<string>("Libelle")
                        .IsRequired();

                    b.Property<int?>("ParentId");

                    b.Property<decimal>("PrixMax");

                    b.Property<decimal>("PrixMin");

                    b.Property<int>("PromoteurId");

                    b.Property<bool>("Recherchable");

                    b.Property<int>("TypeId");

                    b.HasKey("ClassificationId");

                    b.HasIndex("ParentId");

                    b.HasIndex("PromoteurId");

                    b.HasIndex("TypeId");

                    b.ToTable("Classifications");
                });

            modelBuilder.Entity("ASimmo.Models.Local", b =>
                {
                    b.Property<int>("LocalId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdresseId");

                    b.Property<int>("PromoteurId");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("LocalId");

                    b.HasIndex("AdresseId");

                    b.HasIndex("PromoteurId");

                    b.ToTable("Locaux");
                });

            modelBuilder.Entity("ASimmo.Models.Promoteur", b =>
                {
                    b.Property<int>("PromoteurId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Image");

                    b.Property<string>("Libelle");

                    b.Property<int>("TypeId");

                    b.Property<string>("UserId");

                    b.HasKey("PromoteurId");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Promoteurs");
                });

            modelBuilder.Entity("ASimmo.Models.TypeBienImmo", b =>
                {
                    b.Property<int>("TypeBienImmoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Libelle");

                    b.HasKey("TypeBienImmoId");

                    b.ToTable("TypesBiensImmos");

                    b.HasData(
                        new
                        {
                            TypeBienImmoId = 1,
                            Libelle = "Maison"
                        },
                        new
                        {
                            TypeBienImmoId = 2,
                            Libelle = "Appartement"
                        },
                        new
                        {
                            TypeBienImmoId = 3,
                            Libelle = "Terrain"
                        },
                        new
                        {
                            TypeBienImmoId = 4,
                            Libelle = "Studio"
                        },
                        new
                        {
                            TypeBienImmoId = 5,
                            Libelle = "Villa"
                        },
                        new
                        {
                            TypeBienImmoId = 6,
                            Libelle = "Immeuble"
                        },
                        new
                        {
                            TypeBienImmoId = 7,
                            Libelle = "Résidence"
                        });
                });

            modelBuilder.Entity("ASimmo.Models.TypeClassification", b =>
                {
                    b.Property<int>("TypeClassificationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Libelle");

                    b.HasKey("TypeClassificationId");

                    b.ToTable("TypesClassifications");

                    b.HasData(
                        new
                        {
                            TypeClassificationId = 1,
                            Libelle = "Projet"
                        },
                        new
                        {
                            TypeClassificationId = 2,
                            Libelle = "Résidence"
                        },
                        new
                        {
                            TypeClassificationId = 3,
                            Libelle = "Immeuble"
                        });
                });

            modelBuilder.Entity("ASimmo.Models.TypePromoteur", b =>
                {
                    b.Property<int>("TypePromoteurId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Libelle");

                    b.HasKey("TypePromoteurId");

                    b.ToTable("TypesPromoteurs");

                    b.HasData(
                        new
                        {
                            TypePromoteurId = 1,
                            Libelle = "Professionnel"
                        },
                        new
                        {
                            TypePromoteurId = 2,
                            Libelle = "Entreprise"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ASimmo.Models.Adresse", b =>
                {
                    b.HasOne("ASimmo.Models.Promoteur", "Promoteur")
                        .WithMany()
                        .HasForeignKey("PromoteurId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ASimmo.Models.BienImmo", b =>
                {
                    b.HasOne("ASimmo.Models.Adresse", "Adresse")
                        .WithMany()
                        .HasForeignKey("AdresseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ASimmo.Models.Classification", "Classification")
                        .WithMany()
                        .HasForeignKey("ClassificationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ASimmo.Models.TypeBienImmo", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ASimmo.Models.Classification", b =>
                {
                    b.HasOne("ASimmo.Models.Classification", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.HasOne("ASimmo.Models.Promoteur", "Promoteur")
                        .WithMany()
                        .HasForeignKey("PromoteurId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ASimmo.Models.TypeClassification", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ASimmo.Models.Local", b =>
                {
                    b.HasOne("ASimmo.Models.Adresse", "Adresse")
                        .WithMany()
                        .HasForeignKey("AdresseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ASimmo.Models.Promoteur", "Promoteur")
                        .WithMany()
                        .HasForeignKey("PromoteurId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ASimmo.Models.Promoteur", b =>
                {
                    b.HasOne("ASimmo.Models.TypePromoteur", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
