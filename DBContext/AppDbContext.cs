using courseManagementSystemV1.Models;
using Microsoft.EntityFrameworkCore;

namespace courseManagementSystemV1.DBContext
{
    public class AppDbContext : DbContext
    {
        // Add-Migration CMS1 -Context AppDbContext
        //  Update-Database -Context AppDbContext
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<CourseManagement> courseManagements { get; set; }
        public DbSet<Bonus> Bonus { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<HRManagement> HRManagements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }


    }
}
