using E_Learning_Platform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Learning_Platform.Pages
{
    [Authorize(Policy = "StudentOnly")]
    public class SendingResponsesModel : PageModel
    {
        private readonly Data.E_Learning_PlatformContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SendingResponsesModel(Data.E_Learning_PlatformContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public int CourseId { get; set; }

        public IActionResult OnGet()
        {
            if (TempData["CourseId"] != null)
            {
                CourseId = (int)TempData["CourseId"];

                TempData.Keep("CourseId");
            }

            return Page();
        }

        [BindProperty]
        public StudentResponsesModel StudentResponses { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (TempData["CourseId"] != null)
            {
                CourseId = (int)TempData["CourseId"];
            }

            var studentEmail = User.FindFirst(ClaimTypes.Email).Value;

            var folderName = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "StudentResponses", $"{CourseId}", $"{studentEmail}");

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

            await SaveFile(StudentResponses.Quiz1Response, "Quiz1Response");
            await SaveFile(StudentResponses.MidtermResponse, "MidtermResponse");
            await SaveFile(StudentResponses.Quiz2Response, "Quiz2Response");
            await SaveFile(StudentResponses.FinalResponse, "FinalResponse");

            return RedirectToPage($"/BeginedCourse", new {id = CourseId});
        }
    }
}
