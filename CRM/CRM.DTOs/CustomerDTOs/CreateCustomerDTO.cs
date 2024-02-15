using System.ComponentModel.DataAnnotations;
namespace CRM.DTOs.CustomerDTOs
{
    public class CreateCustomerDTO
    {
        [Display(Name ="Nombre")]
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [MaxLength(50, ErrorMessage ="El campo nombre no puede tenr mas de 50 caracteres")]
        public string Name { get; set; }

        [Display(Name ="Apellido")]
        [Required(ErrorMessage ="El campo apellido es oobligatorio")]
        [MaxLength(50, ErrorMessage ="El campo apellido no puede tener mas de 50 caracteres")]
        public string LastName { get; set; }

        [Display(Name ="Direccion")]
        [MaxLength(255, ErrorMessage ="El campo Direccion no puede tener ms 255 caracteres")]
        public string? Address { get; set; }
    }
}
