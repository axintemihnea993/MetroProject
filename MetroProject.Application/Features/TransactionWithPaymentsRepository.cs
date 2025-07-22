using MetroProject.Application.DTOs;
using MetroProject.Application.Repositories;
using MetroProject.Domain;
using MetroProject.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroProject.Application.Features
{
    public class TransactionWithPaymentsRepository
    {
        public TransactionsRepository TransactionReporsitory { get; set; }
        public PaymentsRepository PaymentsRepository { get; set; }
        public ArticlesRepository ArticlesRepository { get; set; }
        private AppDbContext dbContext;
        public TransactionWithPaymentsRepository(AppDbContext context)
        {
            this.dbContext = context;
            this.TransactionReporsitory = new TransactionsRepository(context);
            this.PaymentsRepository = new PaymentsRepository(context);
            this.ArticlesRepository = new ArticlesRepository(context);
        }

        public CheckoutCommandDTO Create(CheckoutCommandDTO transaction)
        {
            try
            {
                var transactionDTO = new TransactionDTO()
                {
                    Timestamp = DateTime.Now,
                };
                var savedTransaction = this.TransactionReporsitory.Create(transactionDTO);
                UpdateArticleQuantiy(transaction.ArticlesQuantity);
                ProcessPayments(transaction.Payments);

                dbContext.SaveChanges();

                transaction.Id = savedTransaction.Id;
                return transaction;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the transaction with payments.", ex);
            }
        }

        private void ProcessPayments(ICollection<PaymentDTO> payments)
        {
            var paymentEntities = this.PaymentsRepository.GetById(payments.Select(p => p.Id).ToList());
            if (paymentEntities == null || !paymentEntities.Any())
            {
                throw new Exception("No payments found for processing.");
            }

            GetResponseForPaymentFromBank();
            
            foreach (var payment in paymentEntities)
            {
                payment.ProcessedTime = DateTime.UtcNow;
                payment.ProcessedStatus = true;
            }
        }

        private bool GetResponseForPaymentFromBank()
        {
            return true;
        }

        private void UpdateArticleQuantiy(KeyValuePair<int, int>[] ArticlesQuantity)
        {
            foreach (var articlesQuantityValuePair in ArticlesQuantity)
            {
                var article = this.ArticlesRepository.GetById(articlesQuantityValuePair.Key);
                article.Stock -= articlesQuantityValuePair.Value;

                this.ArticlesRepository.Update(new ArticleDTO()
                {
                    Id = article.Id,
                    Name = article.Name,
                    Price = article.Price,
                    Description = article.Description,
                    CreatedAt = article.CreatedAt,
                    UpdatedAt = article.UpdatedAt,
                    Stock = article.Stock
                });
            }
        }
    }
}
