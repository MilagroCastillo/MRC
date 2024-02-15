﻿//import el espacio de nombres necesario para DbContext.
using Microsoft.EntityFrameworkCore;
using CRM.API.Models.EN;
// Define la clase CRMContext que hereda de DbContext.
namespace CRM.API.Models.DAL
{
    public class CRMContext : DbContext
    {
        //Constructor que toma DbContextOptions como parametro para configurar la conexion a la base de datos 
        public CRMContext(DbContextOptions<CRMContext>options):base(options)
        {

        }
        //Define un Dbset llamado "Customers" que representa una tabla de clientes en la base de datos 
        public DbSet<Customer> Customers { get; set; }
    }
}