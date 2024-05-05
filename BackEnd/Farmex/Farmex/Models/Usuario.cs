using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FarmexBackEnd.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaNacimiento { get; set; }
                
        public DateTime FechaCreacion { get; set; }

        public int IdPerfil { get; set; }

        public int Activo { get; set; }


    }

}
