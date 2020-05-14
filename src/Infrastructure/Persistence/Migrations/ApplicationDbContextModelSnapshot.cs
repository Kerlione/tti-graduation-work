﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tti_graduation_work.Infrastructure.Persistence;

namespace tti_graduation_work.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("tti_graduation_work.Domain.Entities.Attachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Content")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<int>("StepId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StepId");

                    b.ToTable("Attachements");
                });

            modelBuilder.Entity("tti_graduation_work.Domain.Entities.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExternalId")
                        .HasColumnType("int");

                    b.Property<string>("ShortTitle_EN")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ShortTitle_LV")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ShortTitle_RU")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Title_EN")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("Title_LV")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("Title_RU")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.HasAlternateKey("ExternalId");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("tti_graduation_work.Domain.Entities.FieldOfInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SupervisorId")
                        .HasColumnType("int");

                    b.Property<string>("Title_EN")
                        .HasColumnType("nvarchar(1024)")
                        .HasMaxLength(1024);

                    b.Property<string>("Title_LV")
                        .HasColumnType("nvarchar(1024)")
                        .HasMaxLength(1024);

                    b.Property<string>("Title_RU")
                        .HasColumnType("nvarchar(1024)")
                        .HasMaxLength(1024);

                    b.HasKey("Id");

                    b.HasIndex("SupervisorId");

                    b.ToTable("FieldsOfInterest");
                });

            modelBuilder.Entity("tti_graduation_work.Domain.Entities.GraduationPaper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FacultyId")
                        .HasColumnType("int");

                    b.Property<int>("PaperStatus")
                        .HasColumnType("int");

                    b.Property<int>("PaperType")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int?>("SupervisorId")
                        .HasColumnType("int");

                    b.Property<string>("Title_EN")
                        .HasColumnType("nvarchar(1024)")
                        .HasMaxLength(1024);

                    b.Property<string>("Title_LV")
                        .HasColumnType("nvarchar(1024)")
                        .HasMaxLength(1024);

                    b.Property<string>("Title_RU")
                        .HasColumnType("nvarchar(1024)")
                        .HasMaxLength(1024);

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.HasIndex("SupervisorId");

                    b.ToTable("GraduationPapers");
                });

            modelBuilder.Entity("tti_graduation_work.Domain.Entities.JobPosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title_EN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title_LV")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title_RU")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("JobPositions");
                });

            modelBuilder.Entity("tti_graduation_work.Domain.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title_EN")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("Title_LV")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("Title_RU")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("tti_graduation_work.Domain.Entities.Programe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExternalId")
                        .HasColumnType("int");

                    b.Property<int>("FacultyId")
                        .HasColumnType("int");

                    b.Property<string>("Title_EN")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("Title_LV")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("Title_RU")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.HasAlternateKey("ExternalId");

                    b.HasIndex("FacultyId");

                    b.ToTable("Programes");
                });

            modelBuilder.Entity("tti_graduation_work.Domain.Entities.Step", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GraduationPaperId")
                        .HasColumnType("int");

                    b.Property<string>("StepData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StepStatus")
                        .HasColumnType("int");

                    b.Property<int>("StepType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GraduationPaperId");

                    b.ToTable("Steps");
                });

            modelBuilder.Entity("tti_graduation_work.Domain.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExternalId")
                        .HasColumnType("int");

                    b.Property<int>("GranduationPaperId")
                        .HasColumnType("int");

                    b.Property<int?>("LanguageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("Phone1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProgrameId")
                        .HasColumnType("int");

                    b.Property<string>("Skype")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("ExternalId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("ProgrameId");

                    b.HasIndex("UserId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("tti_graduation_work.Domain.Entities.Supervisor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Degree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExternalId")
                        .HasColumnType("int");

                    b.Property<int>("FacultyId")
                        .HasColumnType("int");

                    b.Property<int>("JobPositionId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentLimit")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("ExternalId");

                    b.HasIndex("FacultyId");

                    b.HasIndex("JobPositionId");

                    b.HasIndex("UserId");

                    b.ToTable("Supervisors");
                });

            modelBuilder.Entity("tti_graduation_work.Domain.Entities.SupervisorLanguage", b =>
                {
                    b.Property<int>("SupervisorId")
                        .HasColumnType("int");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.HasKey("SupervisorId", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("SupervisorLanguages");
                });

            modelBuilder.Entity("tti_graduation_work.Domain.Entities.ThesisTopic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SupervisorId")
                        .HasColumnType("int");

                    b.Property<string>("Title_EN")
                        .HasColumnType("nvarchar(1024)")
                        .HasMaxLength(1024);

                    b.Property<string>("Title_LV")
                        .HasColumnType("nvarchar(1024)")
                        .HasMaxLength(1024);

                    b.Property<string>("Title_RU")
                        .HasColumnType("nvarchar(1024)")
                        .HasMaxLength(1024);

                    b.HasKey("Id");

                    b.HasIndex("SupervisorId");

                    b.ToTable("ThesisTopics");
                });

            modelBuilder.Entity("tti_graduation_work.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasAlternateKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("tti_graduation_work.Domain.Entities.Attachment", b =>
                {
                    b.HasOne("tti_graduation_work.Domain.Entities.Step", "Step")
                        .WithMany("Attachments")
                        .HasForeignKey("StepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("tti_graduation_work.Domain.Entities.FieldOfInterest", b =>
                {
                    b.HasOne("tti_graduation_work.Domain.Entities.Supervisor", "Supervisor")
                        .WithMany("FieldsOfInterest")
                        .HasForeignKey("SupervisorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("tti_graduation_work.Domain.Entities.GraduationPaper", b =>
                {
                    b.HasOne("tti_graduation_work.Domain.Entities.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tti_graduation_work.Domain.Entities.Student", "Student")
                        .WithOne("GraduationPaper")
                        .HasForeignKey("tti_graduation_work.Domain.Entities.GraduationPaper", "StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("tti_graduation_work.Domain.Entities.Supervisor", "Supervisor")
                        .WithMany("GraduationPapers")
                        .HasForeignKey("SupervisorId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("tti_graduation_work.Domain.Entities.Programe", b =>
                {
                    b.HasOne("tti_graduation_work.Domain.Entities.Faculty", "Faculty")
                        .WithMany("Programes")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("tti_graduation_work.Domain.Entities.Step", b =>
                {
                    b.HasOne("tti_graduation_work.Domain.Entities.GraduationPaper", "GraduationPaper")
                        .WithMany("Steps")
                        .HasForeignKey("GraduationPaperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("tti_graduation_work.Domain.Entities.Student", b =>
                {
                    b.HasOne("tti_graduation_work.Domain.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");

                    b.HasOne("tti_graduation_work.Domain.Entities.Programe", "Programe")
                        .WithMany()
                        .HasForeignKey("ProgrameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tti_graduation_work.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("tti_graduation_work.Domain.Entities.Supervisor", b =>
                {
                    b.HasOne("tti_graduation_work.Domain.Entities.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tti_graduation_work.Domain.Entities.JobPosition", "JobPosition")
                        .WithMany("Supervisors")
                        .HasForeignKey("JobPositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tti_graduation_work.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("tti_graduation_work.Domain.Entities.SupervisorLanguage", b =>
                {
                    b.HasOne("tti_graduation_work.Domain.Entities.Language", "Language")
                        .WithMany("SupervisorLanguages")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tti_graduation_work.Domain.Entities.Supervisor", "Supervisor")
                        .WithMany("SupervisorLanguages")
                        .HasForeignKey("SupervisorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("tti_graduation_work.Domain.Entities.ThesisTopic", b =>
                {
                    b.HasOne("tti_graduation_work.Domain.Entities.Supervisor", "Supervisor")
                        .WithMany("ThesisTopics")
                        .HasForeignKey("SupervisorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
