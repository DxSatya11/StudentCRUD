using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.Models;
using System;

namespace StudentAdminPortal.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
           
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Address> Address { get; set; } 
        public DbSet<Gender> Gender { get; set; }   
    }
}
