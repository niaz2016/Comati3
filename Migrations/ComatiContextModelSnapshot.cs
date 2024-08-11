﻿// <auto-generated />
using System;
using Comati3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Comati3.Migrations
{
    [DbContext(typeof(ComatiContext))]
    partial class ComatiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Comati3.Models.Comati", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Per_Head")
                        .HasColumnType("int");

                    b.Property<string>("Remarks")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Start_Date")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Comaties");
                });

            modelBuilder.Entity("Comati3.Models.ComatiMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("ComatiId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("OpeningMonth")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("Remarks")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ComatiId");

                    b.HasIndex("PersonId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Comati3.Models.ComatiPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("ComatiId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Remarks")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ComatiId");

                    b.HasIndex("MemberId");

                    b.ToTable("ComatiPayments");
                });

            modelBuilder.Entity("Comati3.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Mgr")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Remarks")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("Comati3.Models.Comati", b =>
                {
                    b.HasOne("Comati3.Models.Person", "Manager")
                        .WithMany("ComatisManaged")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("Comati3.Models.ComatiMember", b =>
                {
                    b.HasOne("Comati3.Models.Comati", "Comati")
                        .WithMany("Members")
                        .HasForeignKey("ComatiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Comati3.Models.Person", "Person")
                        .WithMany("ComatiMemberships")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comati");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Comati3.Models.ComatiPayment", b =>
                {
                    b.HasOne("Comati3.Models.Comati", "Comati")
                        .WithMany("Payments")
                        .HasForeignKey("ComatiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Comati3.Models.ComatiMember", "ComatiMember")
                        .WithMany("ComatiPayments")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comati");

                    b.Navigation("ComatiMember");
                });

            modelBuilder.Entity("Comati3.Models.Comati", b =>
                {
                    b.Navigation("Members");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Comati3.Models.ComatiMember", b =>
                {
                    b.Navigation("ComatiPayments");
                });

            modelBuilder.Entity("Comati3.Models.Person", b =>
                {
                    b.Navigation("ComatiMemberships");

                    b.Navigation("ComatisManaged");
                });
#pragma warning restore 612, 618
        }
    }
}
