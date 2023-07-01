﻿// <auto-generated />
using LiveLocationService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

#nullable disable

namespace LiveLocationService.Infrastructure.Persistence.Migrations;
[DbContext(typeof(AppDbContext))]
[Migration("20230531133020_Init")]
partial class Init
{
    /// <inheritdoc />
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "7.0.5")
            .HasAnnotation("Proxies:ChangeTracking", false)
            .HasAnnotation("Proxies:CheckEquality", false)
            .HasAnnotation("Proxies:LazyLoading", true)
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

        modelBuilder.Entity("LiveLocationService.Domain.Entities.User", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uuid")
                    .HasColumnName("id");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("character varying(100)")
                    .HasColumnName("name");

                b.Property<string>("Phone")
                    .HasMaxLength(15)
                    .HasColumnType("character varying(15)")
                    .HasColumnName("phone");

                b.Property<int>("Status")
                    .HasColumnType("integer")
                    .HasColumnName("status");

                b.HasKey("Id")
                    .HasName("pk_users");

                b.HasIndex("Phone")
                    .IsUnique()
                    .HasDatabaseName("ix_users_phone");

                b.ToTable("users", (string)null);
            });
#pragma warning restore 612, 618
    }
}