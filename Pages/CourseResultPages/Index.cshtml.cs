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

namespace E_Learning_Platform.Pages.CourseResultPages
{
    public class IndexModel : PageModel
    {
        private readonly E_Learning_Platform.Data.E_Learning_PlatformContext _context;

        public IndexModel(E_Learning_Platform.Data.E_Learning_PlatformContext context)
        {
            _context = context;
        }

        public IList<CourseResults> CourseResults { get;set; } = default!;

        [TempData]
        public string CourseName { get; set; } = default!;

        public async Task OnGetAsync()
        {
            TempData.Keep("CourseName");

            var courseResults = await _context.CourseResults.ToListAsync();

            CourseResults = courseResults
                .Where(result => result.CourseName == CourseName & result.CourseAuthorEmail == User.FindFirst(ClaimTypes.Email).Value)
                .ToList();
        }
    }
}
