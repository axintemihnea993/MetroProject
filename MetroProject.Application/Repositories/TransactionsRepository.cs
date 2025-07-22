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
        private readonly AppDbContext _context;
        public TransactionsRepository(AppDbContext context)
        {
            this._context = context;
        }

        public TransactionDTO Create(TransactionDTO transaction)
        {
            var newTransaction = new Transactions
            {
                Timestamp = transaction.Timestamp,
                // Set Customer, ArticlesTransactions, Payments as needed
            };
            _context.Transactions.Add(newTransaction);

            return new TransactionDTO
            {
                Id = newTransaction.Id,
                Timestamp = newTransaction.Timestamp,
                // Map related DTOs if needed
            };
        }

        public List<TransactionDTO> Get()
        {
            using (var context = new AppDbContext())
            {
                var transactions = context.Transactions
                    .Select(t => new TransactionDTO
                    {
                        Id = t.Id,
                        Timestamp = t.Timestamp,
                        // Map related DTOs if needed
                    })
                    .ToList();

                return transactions;
            }
        }

        public TransactionDTO Update(TransactionDTO transaction)
        {
            using (var context = new AppDbContext())
            {
                var existingTransaction = context.Transactions.Find(transaction.Id);
                if (existingTransaction == null)
                {
                    throw new Exception("Transaction not found");
                }
                existingTransaction.Timestamp = transaction.Timestamp;
                // Update related entities as needed
                context.SaveChanges();
                return new TransactionDTO
                {
                    Id = existingTransaction.Id,
                    Timestamp = existingTransaction.Timestamp,
                    // Map related DTOs if needed
                };
            }
        }

        public bool Delete(int id)
        {
            using (var context = new AppDbContext())
            {
                var transaction = context.Transactions.Find(id);
                if (transaction == null)
                {
                    return false; // Transaction not found
                }
                context.Transactions.Remove(transaction);
                context.SaveChanges();
                return true; // Transaction deleted successfully
            }
        }
    }
}