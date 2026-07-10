namespace E_Learning_Platform.Models
{
    public class CourseResults
    {
        public int Id { get; set; }
        public string StudentEmail { get; set; }
        public string CourseName { get; set; }
        public string CourseAuthorEmail { get; set; }
        public double Quiz1Result { get; set; }
        public double MidtermResult { get; set; }
        public double Quiz2Result { get; set;}
        public double FinalResult { get; set; }
        public double Total { get; set; }

    }
}
