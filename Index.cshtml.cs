using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem.Data;

namespace AttendanceSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public List<Student> Students { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            try
            {
                Students = _context.Students.ToList();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Database error: {ex.Message}";
            }
        }

        // Seed sample students (used by both Seed & Initialize)
        public void AddSampleStudents()
        {
            if (_context.Students.Any()) return;

            var students = new List<Student>
            {
                new() { StudentNumber = "00-00123", FullName = "Luib, Mark Christian" },
                new() { StudentNumber = "00-00456", FullName = "Manggay, Kim Camille" },
                new() { StudentNumber = "00-00789", FullName = "Fruto, Henlyn" },
                new() { StudentNumber = "00-00234", FullName = "Corvera, Erika Mae" },
                new() { StudentNumber = "00-00345", FullName = "Bolivar, Eurika Mae Conception, Dean" },
                new() { StudentNumber = "00-00567", FullName = "Sabelario, Shan" },
                new() { StudentNumber = "00-00678", FullName = "Obando, Zyrill" }
            };

            _context.Students.AddRange(students);
            _context.SaveChanges();
        }

        public IActionResult OnPostSeed()
        {
            try
            {
                AddSampleStudents();
            }
            catch (Exception ex)
            {
                TempData["SeedError"] = "Seeding failed: " + ex.Message;
            }
            return RedirectToPage();
        }

        public IActionResult OnPostInitialize()
        {
            try
            {
                AddSampleStudents();
            }
            catch (Exception ex)
            {
                TempData["SeedError"] = "Adding sample students failed: " + ex.Message;
            }
            return RedirectToPage();
        }
    }
}
