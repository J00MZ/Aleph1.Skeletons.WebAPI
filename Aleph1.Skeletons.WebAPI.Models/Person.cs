using FluentValidation;
using FluentValidation.Attributes;
using System.ComponentModel;

namespace Aleph1.Skeletons.WebAPI.Models
{
    /// <summary>נתוני אדם</summary>
    [Validator(typeof(PersonValidator))]
    public class Person
    {
        /// <summary>מזהה</summary>
        [DisplayName(@"ת""ז")]
        public int ID { get; set; }

        /// <summary>שם פרטי</summary>
        [DisplayName("שם פרטי")]
        public string FirstName { get; set; }

        /// <summary>שם משפחה</summary>
        [DisplayName("שם משפחה")]
        public string LastName { get; set; }
    }
    
    internal class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x.ID).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
        }
    }
}
