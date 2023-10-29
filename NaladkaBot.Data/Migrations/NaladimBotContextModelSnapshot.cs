﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NaladimBot.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NaladimBot.Data.Migrations
{
    [DbContext(typeof(NaladimBotContext))]
    partial class NaladimBotContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("NaladimBot.Data.Entities.Name", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("NameNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NumberId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NumberId");

                    b.ToTable("Names");
                });

            modelBuilder.Entity("NaladimBot.Data.Entities.Number", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Mashine")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("ReadyNumberPhoto")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("StampPhoto")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("TechnicalProcessPhoto")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("Numbers");
                });

            modelBuilder.Entity("NaladimBot.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NaladimBot.Data.Entities.Name", b =>
                {
                    b.HasOne("NaladimBot.Data.Entities.Number", "Number")
                        .WithMany("Names")
                        .HasForeignKey("NumberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Number");
                });

            modelBuilder.Entity("NaladimBot.Data.Entities.Number", b =>
                {
                    b.Navigation("Names");
                });
#pragma warning restore 612, 618
        }
    }
}
