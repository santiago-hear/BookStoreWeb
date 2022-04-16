using Newtonsoft.Json;
using System.Text;
using BookStoreWeb.Model.Entities;
using BookStoreWeb.Models.Entities;
using BookStoreWeb.Models.Interfaces;
using BookStoreWeb.Models.Exceptions;

namespace BookStoreWeb.Models.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly HttpClient Client;
        private readonly IConfiguration _configuration;
        private readonly string UrlBase;
        public AutorRepository()
        {
            Client = new();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
            UrlBase = _configuration["ApiService"];

        }
        public async Task<Operation> DeleteAutor(int id)
        {
            try
            {
                HttpResponseMessage Result = await Client.DeleteAsync($"{UrlBase}/Autor/{id}");
                if (Result.StatusCode.Equals(404))
                {
                    throw new ConnectionErrorException("No se pudo conectar al servicio o el servicio no está disponible");
                }
                string result = await Result.Content.ReadAsStringAsync();
                var operation = JsonConvert.DeserializeObject<Operation>(result);
                if(operation == null)
                {
                    throw new Exception();
                }
                return operation;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Operation> EditAutor(int id, Autor autor)
        {
            try
            {
                string operationJsonString = JsonConvert.SerializeObject(autor);
                var RequestContent = new StringContent(operationJsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage Result = await Client.PutAsync($"{UrlBase}/Autor/{id}", RequestContent);
                if(Result.StatusCode.Equals(404))
                {
                    throw new ConnectionErrorException("No se pudo conectar al servicio o el servicio no está disponible");
                }
                string result = await Result.Content.ReadAsStringAsync();
                var operation = JsonConvert.DeserializeObject<Operation>(result);
                if (operation == null)
                {
                    throw new Exception("Ocurrió un error interno");
                }
                return operation;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Operation> GetAutorById(int id)
        {
            try
            {
                HttpResponseMessage Result = await Client.GetAsync($"{UrlBase}/Autor/{id}");
                if (Result.StatusCode.Equals(404))
                {
                    throw new ConnectionErrorException("No se pudo conectar al servicio o el servicio no está disponible");
                }
                string result = await Result.Content.ReadAsStringAsync();
                var operation = JsonConvert.DeserializeObject<Operation>(result);
                if(operation == null)
                {
                    throw new Exception();
                }
                return operation;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Operation> GetAutors()
        {
            try
            {
                HttpResponseMessage Result = await Client.GetAsync($"{UrlBase}/Autor");
                string result = await Result.Content.ReadAsStringAsync();
                var operation = JsonConvert.DeserializeObject<Operation>(result);
                if (operation == null)
                {
                    throw new Exception("Ocurrió un error interno");
                }
                return operation;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ConnectionErrorException("No se pudo conectar al servicio o el servicio no está disponible");
            }
        }

        public async Task<Operation> InsertAutor(Autor autor)
        {
            try
            {
                string operationJsonString = JsonConvert.SerializeObject(autor);
                var RequestContent = new StringContent(operationJsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage Result = await Client.PostAsync($"{UrlBase}/Autor", RequestContent);
                if (Result.StatusCode.Equals(404))
                {
                    throw new ConnectionErrorException("No se pudo conectar al servicio o el servicio no está disponible");
                }
                string result = await Result.Content.ReadAsStringAsync();
                var operation = JsonConvert.DeserializeObject<Operation>(result);
                if (operation == null)
                {
                    throw new Exception("Ocurrió un error interno");
                }
                return operation;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
