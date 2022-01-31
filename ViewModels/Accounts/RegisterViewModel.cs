using System.ComponentModel.DataAnnotations;

namespace blog.ViewModels.Accounts
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O Nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O Email informado é inválido")]
        public string Email { get; set; }
    }
}
