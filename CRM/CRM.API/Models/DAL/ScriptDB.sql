--Crear la base de datos CRMDB
CREATE DATABASE CRMDB
GO
--Utilizar la base de datos CRMDB
USE CRMDB
GO
--Crear la tabla Customers(anteriormente Clients)
CREATE TABLE Customers
(
Id int identity (1,1) primary key,
Name Varchar (50) not null,
LastName Varchar (50) not null,
Address varchar (255)
)
go
