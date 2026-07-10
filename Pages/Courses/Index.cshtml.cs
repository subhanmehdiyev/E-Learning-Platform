using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using E_Learning_Platform.Data;
using E_Learning_Platform.Models;
using System.Security.Claims;

namespace E_Learning_Platform.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly E_Learning_Platform.Data.E_Learning_PlatformContext _context;

        public IndexModel(E_Learning_Platform.Data.E_Learning_PlatformContext context)
        {
            _context = context;
        }

        public IList<Course> Course { get; set; } = default!;

        [TempData]
        public string CourseName { get; set; } = default!;
      
        public async Task OnGetAsync()
        {
            var courses = await _context.Course.ToListAsync();
            Course = courses
                .Where(course => course.Author == User.FindFirst("FullName")?.Value & course.AuthorEmail == User.FindFirst(ClaimTypes.Email).Value)
                .ToList();
        }

        public IActionResult OnGetSendCourseName(string name)
        {
            CourseName = name;
            return RedirectToPage("/CourseResultPages/Index");
        }
    }
}
