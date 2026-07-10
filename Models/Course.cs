using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning_Platform.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string AuthorEmail { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }
    }
}
