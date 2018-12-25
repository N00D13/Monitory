﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Monitory.Core.DBContext;

namespace Monitory.Core.Migrations
{
    [DbContext(typeof(MonitoryContext))]
    [Migration("20181225084603_AddTaskModel")]
    partial class AddTaskModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Monitory.Core.Models.Account", b =>
                {
                    b.Property<Guid>("AccountID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Role");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("AccountID");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Account");
                });

            modelBuilder.Entity("Monitory.Core.Models.Task", b =>
                {
                    b.Property<Guid>("TaskID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("TaskID");

                    b.HasIndex("TaskID")
                        .IsUnique();

                    b.ToTable("Task");
                });
#pragma warning restore 612, 618
        }
    }
}
