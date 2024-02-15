// Importancia del espacio de nombres para los DTOs relacionados con los clientes
using CRM.DTOs.CustomerDTOs;

namespace CRM.AppWebBlazor.Data
{
    public class CustomerService
    {
        readonly HttpClient _httpClientCRMAPI;

        // constructor que recibe una instancia de IHttpClientFactory para crear el cliente HTTP
        public CustomerService(IHttpClientFactory httpClientFactory)
        {
            _httpClientCRMAPI = httpClientFactory.CreateClient("CRMAPI");
        }
        // metodo para buscar clientes utilizando una solicitud HTTP POST
        public async Task<SerchResultCustomerDTO> Search(SearchQueryCustomerDTO searchQueryCustomerDTO)
        {
            var response = await _httpClientCRMAPI.PostAsJsonAsync("/customer/search", searchQueryCustomerDTO);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<SerchResultCustomerDTO>();
                return result ?? new SerchResultCustomerDTO();
            }
            return new SerchResultCustomerDTO(); // devolver un objeto vacio en caso de error o repuesta no exitosa
        }
         //metodo para obtener un cliente por su ID utilizando una solicitud HTTP GET
         public async Task<GetIdResultCustomerDTO> GetById(int id)
         {
            var response = await _httpClientCRMAPI.GetAsync("/customer/" + id);
            if(response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetIdResultCustomerDTO>();
                return result ?? new GetIdResultCustomerDTO();
            }
            return new GetIdResultCustomerDTO();//Devolver un objeto en caso de error o repuesta no exitosa
         } 
        //metodo para crear un nuevo cliente utiizando una solicitud HTTP POST
        public async Task<int> Create(CreateCustomerDTO createCustomerDTO)
        {
            int result = 0;
            var response = await _httpClientCRMAPI.PostAsJsonAsync("/customer", createCustomerDTO);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (int.TryParse(responseBody, out result) == false)
                    result = 0;
            }
            return result;
        }
        // metodo para editar n cliente existente utilizando una solicitud HTTP PUT
        public async Task<int> Edit(EditCustomerDTO editCustomerDTO)
        {
            int result = 0;
            var response = await _httpClientCRMAPI.PutAsJsonAsync("/customer", editCustomerDTO);
            if(response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (int.TryParse(responseBody, out result) == false)
                    result = 0;
            }
            return result;
        }
        // metodo para eliminar un cliente por su ID utilizando una solicitud HTTP DELETE
        public async Task<int> Delete(int id)
        {
            int result = 0;
            var response = await _httpClientCRMAPI.DeleteAsync("/customer/" + id);
            if(response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (int.TryParse(responseBody, out result) == false)
                    result = 0;
            }
            return result;
        }

    }
}