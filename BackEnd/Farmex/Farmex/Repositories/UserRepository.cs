using Dapper;
using Farmex.Utilities;
using FarmexBackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace Farmex.Repositories
{
    public class UserRepository
    {
        private readonly IDbConnection _connection;

        public UserRepository(DbConnectionFactory dbConnectionFactory)
        {
            _connection = dbConnectionFactory.CreateConnection();
        }

        public async Task<IEnumerable<Perfiles>> GetPerfilesAsync()
        {
            //string query = "SELECT * FROM Perfil";
            string query = "SELECT id_perfil as Id  FROM Perfil";
            
            return await _connection.QueryAsync<Perfiles>(query);
        }


        public async Task<Usuario> AgregarUsuario(Usuario user)
        {
            string query = @"INSERT INTO [dbo].[USUARIO]
                   ([nombre_usuario]
                   ,[password]
                   ,[fecha_nacimiento]
                   ,[fecha_creacion]
                   ,[id_perfil]
                   ,[activo])
             VALUES
                   (@NombreUsuario
                   ,@Password
                   ,@FechaNacimiento
                   ,@FechaCreacion
                   ,@IdPerfil
                   ,@Activo)";
            return await _connection.QueryFirstOrDefaultAsync<Usuario>(query, user);
        }

        public async Task<int> InsertUserAsync(User user)
        {
            string query = "INSERT INTO Users (Name, Email) VALUES (@Name, @Email)";
            return await _connection.ExecuteAsync(query, user);
        }

        // Puedes agregar más métodos según tus necesidades, como actualizar y eliminar usuarios

    }

    public class Perfiles { public int id_perfil { get; set; } public string nombre_perfil { get; set; } }
    public class User
    {
       public int id_usuario   { get; set; }
        public String nombre_usuario   { get; set; }
        public String password   { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int  id_perfil { get; set; }
        public  int activo  { get; set; }

       
    }

}
