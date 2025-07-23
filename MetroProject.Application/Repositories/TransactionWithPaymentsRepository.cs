using MetroProject.Application.DTOs;
using MetroProject.Domain;
using MetroProject.Domain.DTOs;
using MetroProject.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroProject.Application.Repositories
{
    public class TransactionWithPaymentsRepository : IRepository<CheckoutCommandDTO>
    {
        public TransactionsRepository TransactionReporsitory { get; set; }
        public PaymentsRepository PaymentsRepository { get; set; }
        public ArticlesRepository ArticlesRepository { get; set; }
        private AppDbContext dbContext;
        public TransactionWithPaymentsRepository(AppDbContext context)
        {
            dbContext = context;
            TransactionReporsitory = new TransactionsRepository(context);
            PaymentsRepository = new PaymentsRepository(context);
            ArticlesRepository = new ArticlesRepository(context);
        }

        public CheckoutCommandDTO Create(CheckoutCommandDTO transaction)
        {
            try
            {
                var transactionDTO = new TransactionDTO()
                {
                    CreationDate = DateTime.UtcNow,
                    Customer = TransactionReporsitory.GetCustomerById(transaction.CustomerId),
                };

                var savedTransaction = TransactionReporsitory.Create(transactionDTO);
                UpdateArticleQuantiy(transaction.ArticlesQuantity);
                ProcessPayments(transaction.PaymentIds);

                dbContext.SaveChanges();

                transaction.Id = savedTransaction.Id;
                return transaction;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the transaction with payments.", ex);
            }
        }

        private void ProcessPayments(ICollection<int> payments)
        {
            var paymentEntities = PaymentsRepository.GetByIds(payments);
            if (paymentEntities == null || !paymentEntities.Any())
            {
                throw new Exception("No payments found for processing.");
            }

            var status = GetResponseForPaymentFromBank();//trebuie await si async
            
            foreach (var payment in paymentEntities)
            {
                payment.ProcessedTime = DateTime.Now.ToUniversalTime();
                payment.ProcessedStatus = status;
                PaymentsRepository.Update(new PaymentDTO()
                {
                    Id = payment.Id,
                    Amount = payment.Amount,
                    CustomerId = payment.CustomerId,
                    PaymentDate = payment.PaymentDate.ToUniversalTime(),
                    PaymentMethod = payment.PaymentMethod,
                    ProcessedStatus = payment.ProcessedStatus,
                    ProcessedTime = payment.ProcessedTime,
                    TransactionId = payment.TransactionId,
                    CreatedOn = payment.CreatedOn,
                    UpdatedAt = DateTime.UtcNow
                });
            }

        }

        private bool GetResponseForPaymentFromBank()
        {
            return true;
        }

        private void UpdateArticleQuantiy(ArticleQuantiy[] ArticlesQuantity)
        {
            foreach (var articlesQuantityValuePair in ArticlesQuantity)
            {
                var article = ArticlesRepository.GetById(articlesQuantityValuePair.ArticleId);
                article.Stock -= articlesQuantityValuePair.Quantity;

                ArticlesRepository.Update(new ArticleDTO()
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

        public List<CheckoutCommandDTO> Get()
        {
            throw new NotImplementedException();
        }

        public CheckoutCommandDTO Update(CheckoutCommandDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
