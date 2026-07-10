namespace E_Learning_Platform.Models
{
    public class StudentResponsesModel
    {
        public IFormFile Quiz1Response { get; set; }
        public IFormFile MidtermResponse { get; set; }
        public IFormFile Quiz2Response { get; set; }
        public IFormFile FinalResponse { get; set; }
    }
}
