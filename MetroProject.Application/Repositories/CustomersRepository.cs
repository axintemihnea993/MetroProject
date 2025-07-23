using MetroProject.Application.DTOs;
using MetroProject.Domain;
using MetroProject.Domain.Entities;
using MetroProject.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MetroProject.Application.Repositories
{
    public class CustomersRepository : IRepository<CustomerDTO>
    {

        private AppDbContext dbContext;
        public CustomersRepository(AppDbContext context)
        {
            this.dbContext = context;
        }
        public CustomerDTO Create(CustomerDTO customer)
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
            this.dbContext.Customers.Add(newCustomer);
            this.dbContext.SaveChanges();
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

        public List<CustomerDTO> Get()
        {
            var customers = dbContext.Customers
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


        public CustomerDTO Update(CustomerDTO customer)
        {
            var existingCustomer = dbContext.Customers.Find(customer.Id);
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
            dbContext.SaveChanges();
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

        public bool Delete(int id)
        {
            var customer = dbContext.Customers.Find(id);
            if (customer == null)
            {
                return false; // Customer not found
            }
            dbContext.Customers.Remove(customer);
            dbContext.SaveChanges();
            return true; // Customer deleted successfully

        }
    }
}