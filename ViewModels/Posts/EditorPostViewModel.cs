using System.ComponentModel.DataAnnotations;

namespace blog.ViewModels.Posts
{
    public class EditorPostViewModel
    {
        //Validações dos campos
        [Required(ErrorMessage = "O Título é obrigatório")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Este campo tem que possuir no mínimo 3 caracteres")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O Sumário é obrigatório")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Este campo tem que possuir no mínimo 3 caracteres")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "O Body é obrigatório")]

        public string Body { get; set; }

        [Required(ErrorMessage = "O Slug é obrigatório")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Este campo tem que possuir no mínimo 3 caracteres")]
        public string Slug { get; set; }
    }
}
