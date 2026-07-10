using Microsoft.AspNetCore.Identity;

namespace E_Learning_Platform.Models
{
    public class User : IdentityUser
    {
        [PersonalData]
        public string FullName { get; set; }
        [PersonalData]
        public int Age { get; set; }
        [PersonalData]
        public string Position { get; set; }
    }
}
