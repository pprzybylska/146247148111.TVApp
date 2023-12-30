﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _146247148111.TVApp.DAOSQL;

#nullable disable

namespace _146247148111.TVApp.DAOSQL.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20231230211143_Initial-Migration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("_146247.TVApp.DAOSQL.Producer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("producers");
                });

            modelBuilder.Entity("_146247148111.TVApp.DAOSQL.TV", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProducerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Screen")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ScreenSize")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("ProducerId");

                    b.ToTable("TVs");
                });

            modelBuilder.Entity("_146247148111.TVApp.DAOSQL.TV", b =>
                {
                    b.HasOne("_146247.TVApp.DAOSQL.Producer", "Producer")
                        .WithMany()
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producer");
                });
#pragma warning restore 612, 618
        }
    }
}
