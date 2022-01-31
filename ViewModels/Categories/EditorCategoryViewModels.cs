using System.ComponentModel.DataAnnotations;

namespace blog.ViewModels.Categories
{
    public class EditorCategoryViewModels
    {
        //Validações dos campos
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Este campo tem que possuir no mínimo 3 caracteres")]

        public string Name { get; set; }
        [Required(ErrorMessage = "O Slug é obrigatório")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Este campo tem que possuir no mínimo 3 caracteres")]
        public string Slug { get; set; }
    }
}
