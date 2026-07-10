using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using E_Learning_Platform.Data;
using E_Learning_Platform.Models;
using System.Security.Claims;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace E_Learning_Platform.Pages.Courses
{
    public class CreateModel : PageModel
    {
        private readonly E_Learning_Platform.Data.E_Learning_PlatformContext _context;

        public CreateModel(E_Learning_Platform.Data.E_Learning_PlatformContext context)
        {
            _context = context;
        }

        public string userEmail { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Course.Add(Course);
            await _context.SaveChangesAsync();

            TempData["CourseId"] = Course.Id;
            return RedirectToPage("/CourseMaterials");
        }
    }
}

