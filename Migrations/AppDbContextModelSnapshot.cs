﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using courseManagementSystemV1.DBContext;

#nullable disable

namespace courseManagementSystemV1.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("courseManagementSystemV1.Models.Attendance", b =>
                {
                    b.Property<int>("AttendanceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttendanceID"), 1L, 1);

                    b.Property<TimeSpan?>("BreakEndTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan?>("BreakStartTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("CheckInTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan?>("CheckOutTime")
                        .HasColumnType("time");

                    b.Property<int>("EnrollmentID")
                        .HasColumnType("int");

                    b.Property<DateTime>("SessionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("AttendanceID");

                    b.HasIndex("EnrollmentID");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("courseManagementSystemV1.Models.Bonus", b =>
                {
                    b.Property<int>("BonusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BonusID"), 1L, 1);

                    b.Property<decimal>("BonusAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("BonusType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateAwarded")
                        .HasColumnType("datetime2");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.HasKey("BonusID");

                    b.HasIndex("CourseID");

                    b.HasIndex("StudentID");

                    b.ToTable("Bonus");
                });

            modelBuilder.Entity("courseManagementSystemV1.Models.Course", b =>
                {
                    b.Property<int>("CourseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseID"), 1L, 1);

                    b.Property<string>("CourseDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CourseEndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoursePhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseRequirements")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CourseStartDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("CourseTime")
                        .HasColumnType("real");

                    b.Property<bool?>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("whoAcceptedCourse")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseID");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("courseManagementSystemV1.Models.CourseManagement", b =>
                {
                    b.Property<int>("courseMangID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("courseMangID"), 1L, 1);

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("courseID")
                        .HasColumnType("int");

                    b.Property<int>("instructorID")
                        .HasColumnType("int");

                    b.HasKey("courseMangID");

                    b.HasIndex("courseID");

                    b.HasIndex("instructorID");

                    b.ToTable("courseManagements");
                });

            modelBuilder.Entity("courseManagementSystemV1.Models.CourseRating", b =>
                {
                    b.Property<int>("CourseRatingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseRatingID"), 1L, 1);

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CourseRatingDateAt")
                        .HasColumnType("datetime2");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("CourseRatingID");

                    b.HasIndex("CourseID");

                    b.HasIndex("UserID");

                    b.ToTable("courseRatings");
                });

            modelBuilder.Entity("courseManagementSystemV1.Models.Enrollment", b =>
                {
                    b.Property<int>("EnrollmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EnrollmentID"), 1L, 1);

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("EnrollmentID");

                    b.HasIndex("CourseID");

                    b.HasIndex("UserID");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("courseManagementSystemV1.Models.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackId"), 1L, 1);

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<DateTime>("FeedbackDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FeedbackText")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("FeedbackId");

                    b.HasIndex("CourseID");

                    b.HasIndex("UserID");

                    b.ToTable("feedbacks");
                });

            modelBuilder.Entity("courseManagementSystemV1.Models.HRManagement", b =>
                {
                    b.Property<int>("HRManagementID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HRManagementID"), 1L, 1);

                    b.Property<DateTime>("AssignedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("HRManagementID");

                    b.HasIndex("CourseID");

                    b.HasIndex("UserID");

                    b.ToTable("HRManagements");
                });

            modelBuilder.Entity("courseManagementSystemV1.Models.Instructor", b =>
                {
                    b.Property<int>("instructorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("instructorID"), 1L, 1);

                    b.Property<string>("GitHibAccount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("ISDeleted")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<string>("LindenInAccount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("instructorConfPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("instructorCreatedAccount")
                        .HasColumnType("datetime2");

                    b.Property<string>("instructorEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("instructorFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("instructorLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("instructorMiddelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("instructorPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("instructorPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("instructorPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("instructorSSN")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.HasKey("instructorID");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("courseManagementSystemV1.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"), 1L, 1);

                    b.Property<bool?>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsUserHR")
                        .HasColumnType("bit");

                    b.Property<bool>("NormalUser")
                        .HasColumnType("bit");

                    b.Property<string>("UserAddressGov")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCollege")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserConfPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UserCreatedAccount")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserDepartment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEnterCollegeDateTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserMiddelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("UserPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserSSN")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("UserStreet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserUniversity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UserlastVisit")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("userAcceptedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("userBlockedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("userDeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("whoAcceptedUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("whoAdminThisUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("whoBlockedUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("whoDeletedUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("whoHrThisUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("courseManagementSystemV1.Models.VisitHistory", b =>
                {
                    b.Property<int>("VisitHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VisitHistoryId"), 1L, 1);

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<DateTime>("VisitHistoryDate")
                        .HasColumnType("datetime2");

                    b.HasKey("VisitHistoryId");

                    b.HasIndex("UserID");

                    b.ToTable("VisitHistories");
                });

            modelBuilder.Entity("courseManagementSystemV1.Models.Attendance", b =>
                {
                    b.HasOne("courseManagementSystemV1.Models.Enrollment", "Enrollment")
                        .WithMany("Attendances")
                        .HasForeignKey("EnrollmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enrollment");
                });

            modelBuilder.Entity("courseManagementSystemV1.Models.Bonus", b =>
                {
                    b.HasOne("courseManagementSystemV1.Models.Course", "Course")
                        .WithMany("Bonuses")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseManagementSystemV1.Models.User", "Student")
                        .WithMany("Bonuses")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("courseManagementSystemV1.Models.CourseManagement", b =>
                {
                    b.HasOne("courseManagementSystemV1.Models.Course", "course")
                        .WithMany("CourseManagements")
                        .HasForeignKey("courseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseManagementSystemV1.Models.Instructor", "instructor")
                        .WithMany("courseManagements")
                        .HasForeignKey("instructorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("course");

                    b.Navigation("instructor");
                });

            modelBuilder.Entity("courseManagementSystemV1.Models.CourseRating", b =>
                {
                    b.HasOne("courseManagementSystemV1.Models.Course", "Course")
                        .WithMany("courseRatings")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseManagementSystemV1.Models.User", "User")
                        .WithMany("courseRatings")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("courseManagementSystemV1.Models.Enrollment", b =>
                {
                    b.HasOne("courseManagementSystemV1.Models.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseManagementSystemV1.Models.User", "User")
                        .WithMany("Enrollments")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("courseManagementSystemV1.Models.Feedback", b =>
                {
                    b.HasOne("courseManagementSystemV1.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseManagementSystemV1.Models.User", "User")
                        .WithMany("feedbacks")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("courseManagementSystemV1.Models.HRManagement", b =>
                {
                    b.HasOne("courseManagementSystemV1.Models.Course", "Course")
                        .WithMany("hRManagements")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseManagementSystemV1.Models.User", "User")
                        .WithMany("hRManagements")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("courseManagementSystemV1.Models.VisitHistory", b =>
                {
                    b.HasOne("courseManagementSystemV1.Models.User", "User")
                        .WithMany("visitHistories")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("courseManagementSystemV1.Models.Course", b =>
                {
                    b.Navigation("Bonuses");

                    b.Navigation("CourseManagements");

                    b.Navigation("Enrollments");

                    b.Navigation("courseRatings");

                    b.Navigation("hRManagements");
                });

            modelBuilder.Entity("courseManagementSystemV1.Models.Enrollment", b =>
                {
                    b.Navigation("Attendances");
                });

            modelBuilder.Entity("courseManagementSystemV1.Models.Instructor", b =>
                {
                    b.Navigation("courseManagements");
                });

            modelBuilder.Entity("courseManagementSystemV1.Models.User", b =>
                {
                    b.Navigation("Bonuses");

                    b.Navigation("Enrollments");

                    b.Navigation("courseRatings");

                    b.Navigation("feedbacks");

                    b.Navigation("hRManagements");

                    b.Navigation("visitHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
