using System.ComponentModel.DataAnnotations;

namespace MetroProject.Domain.Entities
{
    public class Customers
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? LastPurchaseDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public ICollection<Transactions>? Transactions { get; set; }
    }
}
