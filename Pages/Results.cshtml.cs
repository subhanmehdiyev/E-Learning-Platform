using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using E_Learning_Platform.Data;
using E_Learning_Platform.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace E_Learning_Platform.Pages
{
    [Authorize(Policy = "StudentOnly")]
    public class ResultsModel : PageModel
    {
        private readonly E_Learning_Platform.Data.E_Learning_PlatformContext _context;

        public ResultsModel(E_Learning_Platform.Data.E_Learning_PlatformContext context)
        {
            _context = context;
        }

        public IList<CourseResults> CourseResults { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var courseResults = await _context.CourseResults.ToListAsync();

            CourseResults = courseResults
                .Where(result => result.StudentEmail == User.FindFirst(ClaimTypes.Email).Value)
                .ToList();
        }
    }
}
