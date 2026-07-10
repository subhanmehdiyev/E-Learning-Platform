using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using E_Learning_Platform.Data;
using E_Learning_Platform.Models;
using Microsoft.AspNetCore.Hosting;

namespace E_Learning_Platform.Pages.Courses
{
    public class DeleteModel : PageModel
    {
        private readonly E_Learning_Platform.Data.E_Learning_PlatformContext _context;
        private IWebHostEnvironment _webHostEnvironment;

        public DeleteModel(E_Learning_Platform.Data.E_Learning_PlatformContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Course Course { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FirstOrDefaultAsync(m => m.Id == id);

            if (course is not null)
            {
                Course = course;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FindAsync(id);
            if (course != null)
            {
                Course = course;

                _context.Course.Remove(Course);
                await _context.SaveChangesAsync();
            }

            var folderName = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "CourseMaterials");

            if (Directory.Exists(Path.Combine(folderName, $"{Course.Id}")))
            {
                Directory.Delete(Path.Combine(folderName, $"{Course.Id}"), true);
                Directory.Delete(folderName);
            }

            return RedirectToPage("./Index");
        }
    }
}
