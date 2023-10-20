﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NaladimBot.Data;

#nullable disable

namespace NaladimBot.Data.Migrations
{
    [DbContext(typeof(NaladimBotContext))]
    [Migration("20231020172621_AddedNameTable")]
    partial class AddedNameTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("NaladimBot.Data.Entities.Name", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NameNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("NumberId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("NumberId");

                    b.ToTable("Names");
                });

            modelBuilder.Entity("NaladimBot.Data.Entities.Number", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mashine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ReadyNumberPhoto")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("StampPhoto")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("TechnicalProcessPhoto")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Numbers");
                });

            modelBuilder.Entity("NaladimBot.Data.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NaladimBot.Data.Entities.Name", b =>
                {
                    b.HasOne("NaladimBot.Data.Entities.Number", "Number")
                        .WithMany("NamesNumber")
                        .HasForeignKey("NumberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Number");
                });

            modelBuilder.Entity("NaladimBot.Data.Entities.Number", b =>
                {
                    b.Navigation("NamesNumber");
                });
#pragma warning restore 612, 618
        }
    }
}
