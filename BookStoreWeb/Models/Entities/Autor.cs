using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookStoreWeb.Model.Entities
{
    public class Autor
    {   
        public int AutorId { get; set; }
        [Display(Name ="Nombre")]
        [Required(ErrorMessage = "Ingrese un Nombre")]
        public string? Name { get; set; }
        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "Ingrese un apellido")]
        public string? Lastname { get; set; }
        [Display(Name = "Fecha Nacimiento")]
        [Required(ErrorMessage = "La fecha de nacimineto es obligatoria")]
        [BindProperty, DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        [Display(Name = "Ciudad origen")]
        [Required(ErrorMessage = "La ciudad es obligatoria")]
        public string? City { get; set; }
        [Display(Name = "Correo")]
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo electrónico no cumple con el formato")]
        public string? Email { get; set; }
        public string? Fullname => (Name + " " + Lastname);
    }
}
