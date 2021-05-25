using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Nexto.Commom.Dto
{
    public class BaseDto
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        [DisplayName("Nome")]
        public string Nome { get; set; }
    }
}
