using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Lumia.ViewModels
{
    public class RegisterVM
    {
        [MinLength(5)]
        public string Fullname { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password),Compare(nameof(Password))]

        public string ConfirmPassword { get; set; }
    }
}
