using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreWeb.Model.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        [Display(Name = "Título")]
        [Required(ErrorMessage = "Ingrese el título")]
        public string? Title { get; set; }
        [Display(Name = "Año de publicación")]
        [Range(1000, 2030, ErrorMessage = "El {0} debe estar entre {1} y {2}.")]
        [Required(ErrorMessage = "Ingrese el año de publicación del libro")]
        public string? Year { get; set; }
        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "Seleccione una categoría")]
        public string? Category { get; set; }
        [Display(Name = "Autor")]
        [Required(ErrorMessage = "Seleccione un autor")]
        public virtual Autor? Autor { get; set; }
    }
}
