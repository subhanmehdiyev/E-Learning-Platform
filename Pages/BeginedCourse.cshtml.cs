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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace E_Learning_Platform.Pages
{
    [Authorize(Policy = "StudentOnly")]
    public class BeginedCourseModel : PageModel
    {
        private readonly E_Learning_Platform.Data.E_Learning_PlatformContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BeginedCourseModel(E_Learning_Platform.Data.E_Learning_PlatformContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public Course Course { get; set; } = default!;

        public int CourseId { get; set; } = default!;

        public bool IsVisibleDelete { get; set; } = default!;
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
                CourseId = course.Id;

                var studentEmail = User.FindFirst(ClaimTypes.Email).Value;
                var folderName = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "StudentResponses", $"{CourseId}", $"{studentEmail}");

                IsVisibleDelete = Directory.Exists(folderName);

                return Page();
            }

            return NotFound();
        }

        public IActionResult OnPostAsync([FromQuery]int id)
        {
            TempData["CourseId"] = id;
            TempData.Keep("CourseId");

            return RedirectToPage("/SendingResponses");
        }

        public async Task<IActionResult> OnGetDeleteAsync([FromQuery]int id)
        {
            CourseId = id;

            var studentEmail = User.FindFirst(ClaimTypes.Email).Value;

            var folderName = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "StudentResponses", $"{CourseId}");

            if (Directory.Exists(Path.Combine(folderName, $"{studentEmail}")))
            {
                Directory.Delete(Path.Combine(folderName, $"{studentEmail}"), true);
                Directory.Delete(folderName);
            }

            return RedirectToPage("/BeginedCourse", new { id = CourseId});
        }
    }
}
