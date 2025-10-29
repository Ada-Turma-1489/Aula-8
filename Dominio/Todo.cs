using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dominio
{
    public class Todo
    {
        [Display(Name = "Número")]
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "A descrição é obrigatória!")]
        public string Description { get; set; }

        [Display(Name = "Prioridade")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Priority Priority { get; set; }
    }
}
