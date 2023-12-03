using StudentAdminPortal.Models;
using StudentAdminPortal.ViewModel;

namespace StudentAdminPortal.Repository
{
    public interface IStudentRepository
    {
        Task<Student> AddStudent(UpdateStudentViewModel student);  
        Task <List<Student>> GetAllStudent();
        Task<Student> GetStudent(int id);

        Task<List<Gender>> GetGenderASync(); 

        Task<bool> StudentAlredyExist(int id);
        Task<Student> UpdateStudent(int  id, UpdateStudentViewModel student);
        Task<Student> DeleteStudentAsync(int id);
        Task<string> UploadInage(IFormFile file);
        Task<bool> UpdateImageUri(int id,string ImageUrl);

        

    }
}
