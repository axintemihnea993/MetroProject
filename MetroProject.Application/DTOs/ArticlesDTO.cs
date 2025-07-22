using MetroProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroProject.Application.DTOs
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<ArticlesTransactions> ArticlesTransactions { get; set; }
    }
}
