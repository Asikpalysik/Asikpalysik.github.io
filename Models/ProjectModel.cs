using System.Text.Json;
using System.Text.Json.Serialization;

namespace Portfolio.Models
{
    public class ProjectModel
    {

        [JsonPropertyName("img")]
        public string? Id { get; set; }
        public string? Image { get; set; }
        public string? URL { get; set; }
        public string? Title { get; set; }
        public override string? ToString() => JsonSerializer.Serialize<ProjectModel>(this);

    }
}
