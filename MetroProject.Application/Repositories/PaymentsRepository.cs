using MetroProject.Application.DTOs;
using MetroProject.Domain;
using MetroProject.Domain.Entities;
using MetroProject.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MetroProject.Application.Repositories
{
    public class PaymentsRepository: IRepository<PaymentDTO>
    {
        private AppDbContext dbContext;
        public PaymentsRepository(AppDbContext context)
        {
            dbContext = context;
        }

        public PaymentDTO Create(PaymentDTO payment)
        {
            using (var context = new AppDbContext())
            {
                var newPayment = new Payments
                {
                    CustomerId = payment.CustomerId,
                    PaymentDate = payment.PaymentDate,
                    PaymentMethod = payment.PaymentMethod,
                    TransactionId = payment.TransactionId,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                context.Payments.Add(newPayment);
                context.SaveChanges();
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
        }

        public List<PaymentDTO> Get()
        {
            using (var context = new AppDbContext())
            {
                var payments = context.Payments
                    .Select(p => new PaymentDTO
                    {
                        Id = p.Id,
                        CustomerId = p.CustomerId,
                        PaymentDate = p.PaymentDate,
                        PaymentMethod = p.PaymentMethod,
                        TransactionId = p.TransactionId,
                        CreatedOn = p.CreatedOn,
                        UpdatedAt = p.UpdatedAt
                    })
                    .ToList();

                return payments;
            }
        }

        public PaymentDTO Update(PaymentDTO payment)
        {
            using (var context = new AppDbContext())
            {
                var existingPayment = context.Payments.Find(payment.Id);
                if (existingPayment == null)
                {
                    throw new Exception("Payment not found");
                }
                existingPayment.CustomerId = payment.CustomerId;
                existingPayment.Amount = payment.Amount;
                existingPayment.PaymentDate = payment.PaymentDate;
                existingPayment.PaymentMethod = payment.PaymentMethod;
                existingPayment.TransactionId = payment.TransactionId;
                existingPayment.UpdatedAt = DateTime.UtcNow;
                context.SaveChanges();
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
        }

        public bool Delete(int id)
        {
            using (var context = new AppDbContext())
            {
                var payment = context.Payments.Find(id);
                if (payment == null)
                {
                    return false; // Payment not found
                }
                context.Payments.Remove(payment);
                context.SaveChanges();
                return true; // Payment deleted successfully
            }
        }

        public List<Payments> GetById(List<int> list)
        {
            using (var context = new AppDbContext())
            {
                var payments = context.Payments
                    .Where(p => list.Contains(p.Id))
                    .ToList();
                return payments;
            }
        }
    }
}