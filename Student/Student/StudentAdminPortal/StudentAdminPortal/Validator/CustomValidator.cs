using FluentValidation;
using StudentAdminPortal.Models;
using StudentAdminPortal.Repository;
using StudentAdminPortal.ViewModel;

namespace StudentAdminPortal.Validator
{
    public class CustomValidator : AbstractValidator<UpdateStudentViewModel>
    {
        public CustomValidator(IStudentRepository studentRepository)
        { 
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.DOB).NotEmpty();
            RuleFor(x => x.Age).NotEmpty();
            RuleFor(x => x.PresentAddress).NotEmpty();
            //RuleFor(x => x.ProfilImage).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.GenderID).NotEmpty().Must(id =>
            {
                var genderFroDb = studentRepository.GetGenderASync().Result.ToList().FirstOrDefault( x => x.Id == id);
                if(genderFroDb != null )
                {
                    return true;
                }
                return false;

            }).WithMessage("Gender not Valid");
        }
    }
}
