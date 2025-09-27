using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
    }

    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string StudentNumber { get; set; }
    }

    public class AttendanceRecord
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public bool Present { get; set; }

        public Student Student { get; set; }
    }
}
