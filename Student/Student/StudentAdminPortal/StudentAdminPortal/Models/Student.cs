using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;

namespace StudentAdminPortal.Models
{
    public class Student
    {
        public int Id { get; set; } 
        public string Name { get; set; }      
        public int Age { get; set; }
        public DateTime DOB {  get; set; }  
        public string Email { get; set; }   
        public string? ProfilImage { get; set; }
        public int GenderID { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; }
    }
}
