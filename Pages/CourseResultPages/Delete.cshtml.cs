using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using E_Learning_Platform.Data;
using E_Learning_Platform.Models;

namespace E_Learning_Platform.Pages.CourseResultPages
{
    public class DeleteModel : PageModel
    {
        private readonly E_Learning_Platform.Data.E_Learning_PlatformContext _context;

        public DeleteModel(E_Learning_Platform.Data.E_Learning_PlatformContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CourseResults CourseResults { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseresults = await _context.CourseResults.FirstOrDefaultAsync(m => m.Id == id);

            if (courseresults is not null)
            {
                CourseResults = courseresults;

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

            var courseresults = await _context.CourseResults.FindAsync(id);
            if (courseresults != null)
            {
                CourseResults = courseresults;
                _context.CourseResults.Remove(CourseResults);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
