using StudentAdminPortal.Models;

namespace StudentAdminPortal.ViewModel
{
    public class UpdateStudentViewModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string? ProfilImage { get; set; }
        public int GenderID { get; set; }
        public string PresentAddress { get; set; }

    }
}
