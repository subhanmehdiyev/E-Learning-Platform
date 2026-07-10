using E_Learning_Platform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Learning_Platform.Pages
{
    [Authorize(Policy = "InstructorOnly")]
    public class CourseMaterialsModel : PageModel
    {
        private readonly Data.E_Learning_PlatformContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CourseMaterialsModel(Data.E_Learning_PlatformContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public int CourseId { get; set; }

        public IActionResult OnGet()
        {
            CourseId = Convert.ToInt32(TempData["CourseId"]);
            TempData.Keep();
            return Page();
        }

        [BindProperty]
        public Models.CourseMaterialsModel CourseMaterials { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            var folderName = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "CourseMaterials", $"{TempData["CourseId"]}");

            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }

            async Task SaveFile(IFormFile file, string name)
            {
                if (file != null && file.Length > 0)
                {
                    var filePath = Path.Combine(folderName, name + Path.GetExtension(file.FileName));
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(stream);
                }
            }

            await SaveFile(CourseMaterials.ReadMaterial, "ReadMaterial");
            await SaveFile(CourseMaterials.MidtermExam, "MidtermExam");
            await SaveFile(CourseMaterials.Quiz1, "Quiz1");
            await SaveFile(CourseMaterials.Quiz2, "Quiz2");
            await SaveFile(CourseMaterials.FinalExam, "FinalExam");

            return RedirectToPage("./Courses/Index");
        }
    }
}

