using System.ComponentModel.DataAnnotations;

namespace SaaV.SaaS.WebApi.Models
{
    public class CreateDummyModel
    {
        [Required(ErrorMessage = "Name field is mandatory.")]
        [StringLength(255, ErrorMessage = "Name field must not exceed 255 characters.")]
        public string Name { get; set; }
    }
}
