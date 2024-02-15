using CRM.API.Models.DAL;
using CRM.API.Models.EN;
using CRM.DTOs.CustomerDTOs;



namespace CRM.API.Endpoints
{
    public static class CustomerEndpoint
    {
        // metodo para configurar los endpoints relacionados con los clientes
        public static void AddCustomerEndpoints(this WebApplication app)
        {
            //configurar un endpoint de tipo POST para buscar clientes 
            app.MapPost("/customer/search", async (SearchQueryCustomerDTO customerDTO, CustomerDAL customerDAL) =>
            {
                //crear un objeto 'customer'  a partir de los datos proporcionados
                var customer = new Customer
                {
                    Name= customerDTO.Name_Like != null? customerDTO.Name_Like: String.Empty,
                    LastName= customerDTO.LatName_Like !=null? customerDTO.LatName_Like: String.Empty
                };
                // inicializar una lista de clientes y una variable para contar las filas
                var customers = new List<Customer>();
                int countRow = 0;

                // verificar si se debe enviar la cantidad de filas 
                if(customerDTO.SendRowCount == 2)
                {
                    // Realizar una busqueda de clientes y contra las filas 
                    customers = await customerDAL.Search(customer, skip:customerDTO.Skip, take: customerDTO.Take);
                    if (customers.Count > 0)
                        countRow = await customerDAL.CountSearch(customer);
                }
                else
                {
                    // realizar una busqueda de clientes sin contar las filas
                    customers = await customerDAL.Search(customer, skip: customerDTO.Skip, take: customerDTO.Take);

                }
                //crear un objeto 'SerachResultCustomerDTO' para almacenar los resultados
     
                var customerResult = new SerchResultCustomerDTO
                {
                    Data = new List<SerchResultCustomerDTO.CustomerDTO>(),
                    CountRow = countRow

                };
                //mapear los resultados  a objetos ' CustomerDTO' y agregar al resultado 
                customers.ForEach(s => {
                    customerResult.Data.Add(new SerchResultCustomerDTO.CustomerDTO
                    {
                        Id = s.Id,
                        
                        Name = s.Name,
                        LastName = s.LastName,
                        Address = s.Address,

                    });
                });
                // devolver los resultados
                return customerResult;
            });
            // configurar un endpoint de tipo Get para obtener un cliente por ID
            app.MapGet("/customer/{id}", async (int id, CustomerDAL customerDAL) =>
            {
               //obtener un cliente por ID 
                var customer = await customerDAL.GetById(id);

               //crear un objeto 'GetIdResultCustomerDTO' para almacenar el resultado
                var customerResult = new GetIdResultCustomerDTO
                {
                   Id = customer.Id,
                   Name = customer.Name,
                   LastName = customer.LastName,
                   Address = customer.Address,
                };
                //verificar si se encontro el cliente y devolver la respuesta correspodiente
                if (customerResult.Id > 0)
                    return Results.Ok(customerResult);
                else
                    return Results.NotFound(customerResult);
            });
            // configurar un endpoint de tipo Post para crear un nuevo cliente
            app.MapPost("/customer", async (CreateCustomerDTO customerDTO, CustomerDAL customerDAL) =>
            {
                //crear un objeto 'customer' a partir de los datos proporcionados 
                var customer = new Customer
                {
                    Name = customerDTO.Name,
                    LastName = customerDTO.LastName,
                    Address = customerDTO.Address,
                };
                // Intentar crear el cliente y devolver el resultado correspodiente
                int result = await customerDAL.Create(customer);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });
            //configurar un endpoint de tipo PUT para editar un cliente existente
            app.MapPut("/customer", async (EditCustomerDTO customerDTO, CustomerDAL customerDAL) =>
            {
                //crear un objeto 'Customer' a partir de los datos proporcionados
                var customer = new Customer
                {
                    Id = customerDTO.Id,
                    Name = customerDTO.Name,
                    LastName = customerDTO.LastName,
                    Address = customerDTO.Address
                };
                //intentar editar el cliente y devolver el resultado correspodiente
                int result = await customerDAL.Edit(customer);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });
            //configurar un endpoint de tipo DELETE para eliminar un cliente
            app.MapDelete("/customer/{id}", async (int id, CustomerDAL customerDAL) =>
            {
                //intentar eliminar el cliente y devolver el resultado correspodiente
                int result = await customerDAL.Delete(id);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });

        }
    }
}
