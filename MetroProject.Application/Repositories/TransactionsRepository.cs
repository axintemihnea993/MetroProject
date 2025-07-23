using MetroProject.Application.DTOs;
using MetroProject.Domain;
using MetroProject.Domain.Entities;
using MetroProject.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MetroProject.Application.Repositories
{
    public class TransactionsRepository : IRepository<TransactionDTO>
    {
        private readonly AppDbContext dbContext;
        public TransactionsRepository(AppDbContext context)
        {
            this.dbContext = context;
        }

        public TransactionDTO Create(TransactionDTO transaction)
        {
            var customer = dbContext.Customers.Find(transaction.Customer.Id);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }
            var newTransaction = new Transactions
            {
                Timestamp = transaction.CreationDate,
                Customer = customer,
            };
            dbContext.Transactions.Add(newTransaction);

            return new TransactionDTO
            {
                Id = newTransaction.Id,
                CreationDate = newTransaction.Timestamp,
                // Map related DTOs if needed
            };
        }

        public List<TransactionDTO> Get()
        {
            var transactions = dbContext.Transactions
                .Select(t => new TransactionDTO
                {
                    Id = t.Id,
                    CreationDate = t.Timestamp,
                    // Map related DTOs if needed
                })
                .ToList();

            return transactions;
        }

        public TransactionDTO Update(TransactionDTO transaction)
        {
            var existingTransaction = dbContext.Transactions.Find(transaction.Id);
            if (existingTransaction == null)
            {
                throw new Exception("Transaction not found");
            }
            existingTransaction.Timestamp = transaction.CreationDate;
            // Update related entities as needed
            dbContext.SaveChanges();
            return new TransactionDTO
            {
                Id = existingTransaction.Id,
                CreationDate = existingTransaction.Timestamp,
                // Map related DTOs if needed
            };

        }

        public bool Delete(int id)
        {
            var transaction = dbContext.Transactions.Find(id);
            if (transaction == null)
            {
                return false; // Transaction not found
            }
            dbContext.Transactions.Remove(transaction);
            dbContext.SaveChanges();
            return true; // Transaction deleted successfully

        }

        public CustomerDTO GetCustomerById(int customerId)
        {
            var customer = dbContext.Customers.Find(customerId);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }
            return new CustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                CreatedOn = customer.CreatedOn,
                UpdatedAt = customer.UpdatedAt,
                LastPurchaseDate = customer.LastPurchaseDate,
                Address = customer.Address,
                City = customer.City
            };
        }
    }
}