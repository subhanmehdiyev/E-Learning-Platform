namespace E_Learning_Platform.Models
{
    public class CourseMaterialsModel
    {
        public IFormFile ReadMaterial { get; set; }
        public IFormFile MidtermExam { get; set; }
        public IFormFile Quiz1 { get; set; }
        public IFormFile Quiz2 { get; set; }
        public IFormFile FinalExam { get; set; }
    }
}
