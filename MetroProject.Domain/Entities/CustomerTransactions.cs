using System.ComponentModel.DataAnnotations;

namespace MetroProject.Domain.Entities
{
    public class ArticlesTransactions
    {
        [Key]
        public int AssociationId { get; set; }
        public Articles Articles { get; set; }

        public Transactions Transaction { get; set; }

        public string Notes { get; set; }
    }

}
