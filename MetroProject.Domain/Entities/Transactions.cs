using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MetroProject.Domain.Entities
{
    public class Transactions
    {
        [Key]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Amount { get; set; }
        public Customers Customer { get; set; }
        public ICollection<ArticlesTransactions> ArticlesTransactions { get; set; }
        public ICollection<Payments> Payments{ get; set; }
    }
}
