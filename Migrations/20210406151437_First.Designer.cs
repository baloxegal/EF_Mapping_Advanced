﻿// <auto-generated />
using System;
using EF_Mapping_Advanced;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EF_Mapping_Advanced.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20210406151437_First")]
    partial class First
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EF_Mapping_Advanced.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EF_Mapping_Advanced.Employee", b =>
                {
                    b.HasBaseType("EF_Mapping_Advanced.User");

                    b.Property<byte[]>("RowVersion")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EF_Mapping_Advanced.Manager", b =>
                {
                    b.HasBaseType("EF_Mapping_Advanced.User");

                    b.Property<string>("Departament")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("EF_Mapping_Advanced.Employee", b =>
                {
                    b.HasOne("EF_Mapping_Advanced.User", null)
                        .WithOne()
                        .HasForeignKey("EF_Mapping_Advanced.Employee", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EF_Mapping_Advanced.Manager", b =>
                {
                    b.HasOne("EF_Mapping_Advanced.User", null)
                        .WithOne()
                        .HasForeignKey("EF_Mapping_Advanced.Manager", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
