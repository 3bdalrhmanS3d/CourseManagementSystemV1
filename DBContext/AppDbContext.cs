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
        public DbSet<VisitHistory> VisitHistories { get; set; }
        public DbSet<CourseRating> courseRatings { get; set; }
        public DbSet<Feedback> feedbacks { get; set; }
        public DbSet<Comments> comments { get; set; }
        public DbSet<UpdateHistory> updateHistories { get; set; }
        public DbSet<CourseSpecificQuestions> courseSpecificQuestions {  get; set; }  
        public DbSet<Grade> grades { get; set; }
        public DbSet<Quiz> quizzes { get; set; }
        public DbSet<QuizAnswer> quizAnswers { get; set; }
        public DbSet<QuizMeta> quizMeta { get; set; }
        public DbSet<QuizQuestion> quizQuestions { get; set; }
        public DbSet<Take> takes { get; set; }
        public DbSet<TakeAnswer> takesAnswer { get; set; }
        public DbSet<ResourceFile> resourceFiles { get; set; }
        public DbSet<CourseRequirement> courseRequirements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}
