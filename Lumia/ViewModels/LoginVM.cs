using System.ComponentModel.DataAnnotations;

namespace Lumia.ViewModels;

public class LoginVM
{
    [Required]
    public string UserName { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
