﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using scrimp.Entities;

namespace scrimp.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("scrimp.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<string>("CurrencyCode");

                    b.Property<decimal>("CurrentBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CurrentBalanceDate");

                    b.Property<bool>("IsNetWorth");

                    b.Property<string>("Name");

                    b.Property<int>("Type");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("scrimp.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId");

                    b.Property<string>("Color");

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<bool>("IsTransfer");

                    b.Property<string>("Name");

                    b.Property<int?>("ParentId");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("scrimp.Entities.Error", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Detail");

                    b.Property<int?>("HttpRequestId");

                    b.Property<Guid?>("InnerExceptionId");

                    b.Property<int>("Status");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("HttpRequestId");

                    b.HasIndex("InnerExceptionId");

                    b.ToTable("Errors");
                });

            modelBuilder.Entity("scrimp.Entities.HttpRequestMeta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Method");

                    b.Property<string>("Path");

                    b.Property<string>("PathBase");

                    b.HasKey("Id");

                    b.ToTable("HttpRequestMeta");
                });

            modelBuilder.Entity("scrimp.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("AnticipatedDate");

                    b.Property<int?>("CategoryId");

                    b.Property<string>("CheckNumber");

                    b.Property<decimal>("ClosingBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsTransfer");

                    b.Property<string>("Memo");

                    b.Property<string>("Note");

                    b.Property<string>("Payee");

                    b.Property<int>("Status");

                    b.Property<int>("TransactionAccountId");

                    b.Property<int>("Type");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("TransactionAccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("scrimp.Entities.TransactionAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountId");

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<string>("CurrencyCode");

                    b.Property<decimal>("CurrentBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CurrentBalanceDate");

                    b.Property<string>("Name");

                    b.Property<string>("Number");

                    b.Property<decimal>("StartingBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartingBalanceDate");

                    b.Property<int>("Status");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("TransactionAccounts");
                });

            modelBuilder.Entity("scrimp.Entities.TransactionLabel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("TransactionId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionLabel");
                });

            modelBuilder.Entity("scrimp.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<string>("CurrencyCode");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FirstName");

                    b.Property<DateTime?>("LastActivityAt");

                    b.Property<DateTime?>("LastLoggedInAt");

                    b.Property<string>("LastName");

                    b.Property<string>("Timezone");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<int>("WeekStartDay");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("scrimp.Helpers.AppException", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("HResult");

                    b.Property<string>("HelpLink");

                    b.Property<string>("Source");

                    b.HasKey("Id");

                    b.ToTable("AppException");
                });

            modelBuilder.Entity("scrimp.Entities.Category", b =>
                {
                    b.HasOne("scrimp.Entities.Category")
                        .WithMany("Children")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("scrimp.Entities.Error", b =>
                {
                    b.HasOne("scrimp.Entities.HttpRequestMeta", "HttpRequest")
                        .WithMany()
                        .HasForeignKey("HttpRequestId");

                    b.HasOne("scrimp.Helpers.AppException", "InnerException")
                        .WithMany()
                        .HasForeignKey("InnerExceptionId");
                });

            modelBuilder.Entity("scrimp.Entities.Transaction", b =>
                {
                    b.HasOne("scrimp.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("scrimp.Entities.TransactionAccount", "TransactionAccount")
                        .WithMany()
                        .HasForeignKey("TransactionAccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("scrimp.Entities.TransactionAccount", b =>
                {
                    b.HasOne("scrimp.Entities.Account")
                        .WithMany("TransactionAccounts")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("scrimp.Entities.TransactionLabel", b =>
                {
                    b.HasOne("scrimp.Entities.Transaction")
                        .WithMany("Labels")
                        .HasForeignKey("TransactionId");
                });
#pragma warning restore 612, 618
        }
    }
}
