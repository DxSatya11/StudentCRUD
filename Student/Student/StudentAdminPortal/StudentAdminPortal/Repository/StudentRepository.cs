using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StudentAdminPortal.Data;
using StudentAdminPortal.Models;
using StudentAdminPortal.ViewModel;
using System.Collections.Generic;

namespace StudentAdminPortal.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public StudentRepository(StudentDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }



        public async Task<Student> AddStudent(UpdateStudentViewModel student)
        {
            Student model = new Student();
            model.Name = student.Name;  
            model.Email = student.Email;
            model.Age = student.Age;
            model.ProfilImage = student.ProfilImage;    
            model.GenderID = student.GenderID;
            model.Address = new Address
            { 
                PresentAddress = student.PresentAddress 
            };

           await _dbContext.Students.AddAsync(model);
           await _dbContext.SaveChangesAsync();

            return model;

        }


        public async Task<List<Student>> GetAllStudent()
        {
           return await _dbContext.Students.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();  
        }

        public async Task<List<Gender>> GetGenderASync()
        {
            return await _dbContext.Gender.ToListAsync();
        }

        public async Task<Student> GetStudent(int id)
        {
            return await _dbContext.Students.Where(x => x.Id == id).Include(nameof(Gender)).Include(nameof(Address)).FirstOrDefaultAsync();
        }

        public async Task<bool> StudentAlredyExist(int id)
        {
            return await _dbContext.Students.AnyAsync(Gender => Gender.Id == id);
        }

        public async Task<Student> UpdateStudent(int id, UpdateStudentViewModel student)
        {
            var students = await _dbContext.Students.Where(x => x.Id == id).Include(nameof(Address))
               .FirstOrDefaultAsync();

            if(students != null)
            {
                students.Name = student.Name; 
                students.Age = student.Age; 
                students.Email = student.Email;
                students.ProfilImage = student.ProfilImage; 
                students.Address.PresentAddress = student.PresentAddress; 
                students.DOB = student.DOB; 
                students.GenderID= student.GenderID;

                await _dbContext.SaveChangesAsync();

                return students;
            }

            return null;
        }

        public async Task<Student> DeleteStudentAsync(int id)
        {
            var student = await GetStudent(id);
            if(student != null)
            {
                _dbContext.Students.Remove(student); 
                await _dbContext.SaveChangesAsync(); 
                return student;
            }
            return null;

        }

        //public async Task<string> UploadInage(IFormFile file)
        //{
        //    var special = Guid.NewGuid().ToString();
        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"StudentsResorces\Image", special + "-" + file.FileName);

        //    using (FileStream ms = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(ms);
        //    }

        //    var filename = special + "-" + file.FileName;
        //    return Path.Combine(@"StudentsResorces\Image", filename);


        //}


        //public async Task<bool> UpdateImageUri(int id, string ImageUrl)
        //{
        //   var student = await GetStudent(id); 
        //    if(student != null)
        //    {
        //        student.ProfilImage = ImageUrl;
        //        await _dbContext.SaveChangesAsync();
        //        return true;
        //    }
        //    return false;
        //}   





        //blobs upload
        //public async Task<string> UploadInage(IFormFile file)
        //{
        //    var special = Guid.NewGuid().ToString();

        //    var containerName = "studentimage";

        //    var blobName = special + "-" + file.FileName;
        //    string connectionString = _configuration.GetConnectionString("AzureBlobStorage");

        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"StudentsResorces\Image", special + "-" + file.FileName);

        //    using (FileStream ms = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(ms);
        //    }

        //    var filename = special + "-" + file.FileName;
        //    return Path.Combine(@"StudentsResorces\Image", filename);


        //}


        //public async Task<string> UploadImage(IFormFile file)
        //{
        //    var special = Guid.NewGuid().ToString();
        //    var containerName = "studentimage"; // Replace with your Azure Blob Storage container name
        //    string connectionString = _configuration.GetConnectionString("AzureBlobStorage");

        //    var blobServiceClient = new BlobServiceClient(connectionString);
        //    var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        //    var blobName = special + "-" + file.FileName;
        //    var blobClient = containerClient.GetBlobClient(blobName);

        //    using (Stream stream = file.OpenReadStream())
        //    {
        //        await blobClient.UploadAsync(stream, true);
        //    }

        //    return blobClient.Uri.ToString();
        //}


        public async Task<bool> UpdateImageUri(int id, string ImageUrl)
        {
            var student = await GetStudent(id);
            if (student != null)
            {
                student.ProfilImage = ImageUrl;
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<string> UploadInage(IFormFile file)
        {
            var special = Guid.NewGuid().ToString();
            var containerName = "studentimage";
            string connectionString = _configuration.GetConnectionString("AzureBlobStorage");

            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobName = special + "-" + file.FileName;
            var blobClient = containerClient.GetBlobClient(blobName);

            using (Stream stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            return blobClient.Uri.ToString();
        }
    }
}
