using System.Text.Json.Serialization;

namespace Dominio
{
    public class Todo
    {
        public int Id { get; set; }

        public string Description { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Priority Priority { get; set; }
    }
}
