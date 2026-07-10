using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using E_Learning_Platform.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace E_Learning_Platform.Data
{
    public class E_Learning_PlatformContext : IdentityDbContext<User>
    {
        public E_Learning_PlatformContext (DbContextOptions<E_Learning_PlatformContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Course { get; set; } = default!;
        public DbSet<CourseResults> CourseResults { get; set; } = default!;
    }
}
