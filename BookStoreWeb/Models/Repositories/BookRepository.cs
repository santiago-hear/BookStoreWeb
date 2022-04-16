using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using BookStoreWeb.Model.Entities;
using BookStoreWeb.Models.Entities;
using BookStoreWeb.Models.Interfaces;
using BookStoreWeb.Models.Exceptions;

namespace BookStoreWeb.Models.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly HttpClient Client;
        private readonly IConfiguration _configuration;
        private readonly string UrlBase;
        public BookRepository()
        {
            Client = new();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
            UrlBase = _configuration["ApiService"];
        }
        public async Task<Operation> DeleteBook(int id)
        {
            try
            {
                HttpResponseMessage Result = await Client.DeleteAsync($"{UrlBase}/Book/{id}");
                if (Result.StatusCode.Equals(404))
                {
                    throw new Exception("No se pudo conectar al servicio o el servicio no está disponible");
                }
                string result = await Result.Content.ReadAsStringAsync();
                var operation = JsonConvert.DeserializeObject<Operation>(result);
                if (operation == null)
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

        public async Task<Operation> EditBook(int id, Book book)
        {
            try
            {
                JObject bookInsert = new()
                {
                    ["title"] = book.Title,
                    ["year"] = book.Year,
                    ["category"] = book.Category,
                    ["autorId"] = book.Autor.AutorId
                };
                string operationJsonString = JsonConvert.SerializeObject(bookInsert);
                var RequestContent = new StringContent(operationJsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage Result = await Client.PutAsync($"{UrlBase}/Book/{id}", RequestContent);
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

        public async Task<Operation> GetBookById(int id)
        {
            try
            {
                HttpResponseMessage Result = await Client.GetAsync($"{UrlBase}/Book/{id}");
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

        public async Task<Operation> GetBooks()
        {
            try
            {
                HttpResponseMessage Result = await Client.GetAsync($"{UrlBase}/Book");
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

        public async Task<Operation> InsertBook(Book book)
        {
            try
            {
                JObject bookInsert = new()
                {
                    ["title"] = book.Title,
                    ["year"] = book.Year,
                    ["category"] = book.Category,
                    ["autorId"] = book.Autor.AutorId
                };
                string operationJsonString = JsonConvert.SerializeObject(bookInsert);
                var RequestContent = new StringContent(operationJsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage Result = await Client.PostAsync($"{UrlBase}/Book", RequestContent);
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
