//Método para fins didáticos, para armazenar imagens é aconselhável usar um Storage para não sobrecarregar a base de dados

using System.ComponentModel.DataAnnotations;

namespace blog.ViewModels.Accounts
{
    public class UploadImageViewModel
    {
        [Required(ErrorMessage = "Imagem Inválida")]
        public string Base64Image { get; set; }
    }
}
