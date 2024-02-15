using System.ComponentModel.DataAnnotations;
namespace CRM.DTOs.CustomerDTOs
{
    public class SearchQueryCustomerDTO
    {
        [Display(Name = "Nombre")]
        public string? Name_Like { get; set; }
        [Display(Name = "Apellido")]
        public string? LatName_Like { get; set; }
        [Display(Name = "Pagina")]
        public int Skip { get; set; }
        [Display(Name = "CantReg X Pagina")]
        public int Take { get; set; }
        /// <summary>
        /// 1 = No se cuenta loss resultados de la busqueda 
        /// 2 = Cuenta los resultados de la busqueda
        /// </summary>
        public byte SendRowCount { get; set; }
    }
}
