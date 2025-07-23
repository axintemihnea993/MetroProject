using MetroProject.Application.DTOs;
using MetroProject.Domain;
using MetroProject.Domain.Entities;
using MetroProject.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MetroProject.Application.Repositories
{
    public class PaymentsRepository : IRepository<PaymentDTO>
    {
        private AppDbContext dbContext;
        public PaymentsRepository(AppDbContext context)
        {
            dbContext = context;
        }

        public PaymentDTO Create(PaymentDTO payment)
        {
            var newPayment = new Payments
            {
                CustomerId = payment.CustomerId,
                PaymentDate = payment.PaymentDate.ToUniversalTime(),
                PaymentMethod = payment.PaymentMethod,
                TransactionId = payment.TransactionId,
                CreatedOn = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            
            dbContext.Payments.Add(newPayment);
            dbContext.SaveChanges();

            return new PaymentDTO
            {
                Id = newPayment.Id,
                CustomerId = newPayment.CustomerId,
                PaymentDate = newPayment.PaymentDate,
                PaymentMethod = newPayment.PaymentMethod,
                TransactionId = newPayment.TransactionId,
                CreatedOn = newPayment.CreatedOn,
                UpdatedAt = newPayment.UpdatedAt
            };
        }

        public List<PaymentDTO> Get()
        {
            var payments = dbContext.Payments
                .Select(p => new PaymentDTO
                {
                    Id = p.Id,
                    CustomerId = p.CustomerId,
                    PaymentDate = p.PaymentDate,
                    PaymentMethod = p.PaymentMethod,
                    ProcessedStatus = p.ProcessedStatus,
                    ProcessedTime = p.ProcessedTime,
                    TransactionId = p.TransactionId,
                    CreatedOn = p.CreatedOn,
                    UpdatedAt = p.UpdatedAt
                })
                .ToList();

            return payments;
        }

        public PaymentDTO Update(PaymentDTO payment)
        {
            var existingPayment = dbContext.Payments.Find(payment.Id);
            if (existingPayment == null)
            {
                throw new Exception("Payment not found");
            }
            
            existingPayment.CustomerId = payment.CustomerId;
            existingPayment.Amount = payment.Amount;
            existingPayment.PaymentDate = payment.PaymentDate.ToUniversalTime();
            existingPayment.PaymentMethod = payment.PaymentMethod;
            existingPayment.TransactionId = payment.TransactionId;
            existingPayment.ProcessedStatus = payment.ProcessedStatus;
            existingPayment.ProcessedTime = payment.ProcessedTime.ToUniversalTime();
            existingPayment.UpdatedAt = DateTime.UtcNow;
            
            dbContext.SaveChanges();

            return new PaymentDTO
            {
                Id = existingPayment.Id,
                CustomerId = existingPayment.CustomerId,
                PaymentDate = existingPayment.PaymentDate,
                PaymentMethod = existingPayment.PaymentMethod,
                TransactionId = existingPayment.TransactionId,
                CreatedOn = existingPayment.CreatedOn,
                UpdatedAt = existingPayment.UpdatedAt
            };

        }

        public bool Delete(int id)
        {
            var payment = dbContext.Payments.Find(id);
            if (payment == null)
            {
                return false; // Payment not found
            }

            dbContext.Payments.Remove(payment);
            dbContext.SaveChanges();
            
            return true; // Payment deleted successfully

        }

        public List<Payments> GetByIds(ICollection<int> list)
        {
            var payments = dbContext.Payments
                .Where(p => list.Contains(p.Id))
                .ToList();

            return payments;
        }
    }
}