using MetroProject.Application.DTOs;
using MetroProject.Domain;
using MetroProject.Domain.Entities;
using MetroProject.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MetroProject.Application.Repositories
{
    public class CustomersRepository: IRepository<CustomerDTO>
    {
        public CustomerDTO Create(CustomerDTO customer)
        {
            using (var context = new AppDbContext())
            {
                var newCustomer = new Customers
                {
                    Name = customer.Name,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    LastPurchaseDate = customer.LastPurchaseDate,
                    Address = customer.Address,
                    City = customer.City
                };
                context.Customers.Add(newCustomer);
                context.SaveChanges();
                return new CustomerDTO
                {
                    Id = newCustomer.Id,
                    Name = newCustomer.Name,
                    Email = newCustomer.Email,
                    Phone = newCustomer.Phone,
                    CreatedOn = newCustomer.CreatedOn,
                    UpdatedAt = newCustomer.UpdatedAt,
                    LastPurchaseDate = newCustomer.LastPurchaseDate,
                    Address = newCustomer.Address,
                    City = newCustomer.City
                };
            }
        }

        public List<CustomerDTO> Get()
        {
            using (var context = new AppDbContext())
            {
                var customers = context.Customers
                    .Select(c => new CustomerDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Email = c.Email,
                        Phone = c.Phone,
                        CreatedOn = c.CreatedOn,
                        UpdatedAt = c.UpdatedAt,
                        LastPurchaseDate = c.LastPurchaseDate,
                        Address = c.Address,
                        City = c.City
                    })
                    .ToList();

                return customers;
            }
        }

        public CustomerDTO Update(CustomerDTO customer)
        {
            using (var context = new AppDbContext())
            {
                var existingCustomer = context.Customers.Find(customer.Id);
                if (existingCustomer == null)
                {
                    throw new Exception("Customer not found");
                }
                existingCustomer.Name = customer.Name;
                existingCustomer.Email = customer.Email;
                existingCustomer.Phone = customer.Phone;
                existingCustomer.UpdatedAt = DateTime.UtcNow;
                existingCustomer.LastPurchaseDate = customer.LastPurchaseDate;
                existingCustomer.Address = customer.Address;
                existingCustomer.City = customer.City;
                context.SaveChanges();
                return new CustomerDTO
                {
                    Id = existingCustomer.Id,
                    Name = existingCustomer.Name,
                    Email = existingCustomer.Email,
                    Phone = existingCustomer.Phone,
                    CreatedOn = existingCustomer.CreatedOn,
                    UpdatedAt = existingCustomer.UpdatedAt,
                    LastPurchaseDate = existingCustomer.LastPurchaseDate,
                    Address = existingCustomer.Address,
                    City = existingCustomer.City
                };
            }
        }

        public bool Delete(int id)
        {
            using (var context = new AppDbContext())
            {
                var customer = context.Customers.Find(id);
                if (customer == null)
                {
                    return false; // Customer not found
                }
                context.Customers.Remove(customer);
                context.SaveChanges();
                return true; // Customer deleted successfully
            }
        }
    }
}