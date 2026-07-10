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

namespace E_Learning_Platform.Pages
{
    [Authorize(Policy = "StudentOnly")]
    public class CoursesInCardsModel : PageModel
    {
        private readonly E_Learning_Platform.Data.E_Learning_PlatformContext _context;

        public CoursesInCardsModel(E_Learning_Platform.Data.E_Learning_PlatformContext context)
        {
            _context = context;
        }

        public IList<Course> Course { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Course = await _context.Course.ToListAsync();
        }
    }
}

