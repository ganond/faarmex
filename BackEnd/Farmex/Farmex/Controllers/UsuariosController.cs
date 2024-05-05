using Farmex.Repositories;
using FarmexBackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FarmexBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UsuariosController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // POST: api/Usuarios/Registrar
        [HttpPost("Registrar")]
        public async Task<IActionResult> Registrar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new Usuario
                    {
                        NombreUsuario = usuario.NombreUsuario,
                        Password = usuario.Password,
                        FechaNacimiento = usuario.FechaNacimiento,
                        FechaCreacion = DateTime.Now,
                        IdPerfil = usuario.IdPerfil,
                        Activo = 1
                    };

                    await _userRepository.AgregarUsuario(user);
                    return Ok(new { message = "Usuario registrado exitosamente" });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error interno del servidor: {ex.Message}");
                }
            }
            return BadRequest(ModelState);
        }

        // GET: api/Usuarios/Perfiles
        /* [HttpGet("Perfiles")]
         public IActionResult ObtenerPerfiles()
         {
             // Aquí puedes obtener la lista de perfiles desde la base de datos
             // Por ejemplo:
             // var perfiles = dbContext.Perfiles.ToList();

             //var users = await _userRepository.GetAllUsersAsync();
             var perfiles = new List<Perfil>
             {
                 new Perfil { IdPerfil = 1, NombrePerfil = "ADMIN" },
                 new Perfil { IdPerfil = 2, NombrePerfil = "VISUALIZADOR" }
             };
             return Ok(perfiles);
         }*/
        // Endpoint para obtener todos los usuarios
        [HttpGet("Perfiles")]
        public async Task<IActionResult> GetPerfiles()
        {
            try
            {
                var users = await _userRepository.GetPerfilesAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al recuperar los usuarios: {ex.Message}");
            }
        }
    }

}
