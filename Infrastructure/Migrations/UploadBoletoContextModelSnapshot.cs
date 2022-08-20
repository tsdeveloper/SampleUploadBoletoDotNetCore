﻿// <auto-generated />
using System;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(UploadBoletoContext))]
    partial class UploadBoletoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("Core.Entities.UploadBoleto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CodigoAtivo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CodigoCliente")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Corretora")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataOperacao")
                        .HasColumnType("TEXT");

                    b.Property<string>("IdBolsa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PrecoUnitario")
                        .HasColumnType("decimal(6,2)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TipoOperacao")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UploadBoletos", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
