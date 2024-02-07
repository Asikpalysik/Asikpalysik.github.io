using Portfolio.Models;
using System.Text.Json;

namespace Portfolio.Services
{
    public class JsonFileProductService
    {
        private readonly IWebHostEnvironment WebHostEnvironment;
        private readonly JsonSerializerOptions JsonSerializerOptions;

        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
            JsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "projects.json"); }
        }

        public IEnumerable<ProjectModel> GetProducts()
        {
            try
            {
                using var jsonFileReader = File.OpenText(JsonFileName);
                var projects = JsonSerializer.Deserialize<ProjectModel[]>(jsonFileReader.ReadToEnd(), JsonSerializerOptions);

                return projects ?? [];
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while reading JSON file.", ex);
            }
        }
    }
}
