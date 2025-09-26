using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;  
using AttendanceSystem.Models;
using AttendanceSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceSystem.Pages.Attendance
{
    public class MarkModel(AppDbContext context) : PageModel
    {
        private readonly AppDbContext _context = context;

        [BindProperty]
        public int StudentId { get; set; }

        [BindProperty]
        public bool IsPresent { get; set; }

        public List<AttendanceSystem.Data.Student> Students { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            Students = await _context.Students.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Students = await _context.Students.ToListAsync(); // Ensure Students is populated for the page

            var student = await _context.Students.FindAsync(StudentId);
            if (student == null)
            {
                ModelState.AddModelError("", "Student not found.");
                return Page();
            }

            var attendance = new AttendanceSystem.Data.AttendanceRecord
            {
                StudentId = StudentId,
                Date = DateTime.Today,
                Present = IsPresent
            };

            _context.AttendanceRecords.Add(attendance);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Attendance marked successfully!";
            return RedirectToPage("/Attendance/Mark");
        }
    }
}
