using E_Learning_Platform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Learning_Platform.Pages
{ 
    [Authorize(Policy = "InstructorOnly")]
    public class GettingResponseModel : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GettingResponseModel(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public int? CourseId { get; set; } = default!;

        public IList<string> StudentEmails { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CourseId = id;

            if (CourseId is not null)
            {
                var responsesDirectoryPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "StudentResponses", $"{CourseId}");

                if (Directory.Exists(responsesDirectoryPath))
                {
                    StudentEmails = Directory.GetDirectories(responsesDirectoryPath).Select(path => path.Split(['\\']).Last()).ToList();
                }
                else
                {
                    StudentEmails = new List<string>();
                }

                    return Page();
            }

            return NotFound();
        }
    }
}
