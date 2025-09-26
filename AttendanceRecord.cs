namespace AttendanceSystem.Models
{
    public class AttendanceRecord
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }

        // Navigation property
        public Student? Student { get; set; }
    }
}
