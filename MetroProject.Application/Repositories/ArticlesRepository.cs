using MetroProject.Application.DTOs;
using MetroProject.Domain;
using MetroProject.Domain.Entities;
using MetroProject.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroProject.Application.Repositories
{
    public class ArticlesRepository : IRepository<ArticleDTO>
    {
        private AppDbContext dbContext;
        public ArticlesRepository(AppDbContext context)
        {
            dbContext = context;
        }
        public ArticleDTO Create(ArticleDTO article)
        {
            using (var context = new AppDbContext())
            {
                var newArticle = new Articles
                {
                    Name = article.Name,
                    Price = article.Price,
                    Description = article.Description,
                    Stock = article.Stock,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                context.Articles.Add(newArticle);
                context.SaveChanges();
                return new ArticleDTO
                {
                    Id = newArticle.Id,
                    Name = newArticle.Name,
                    Price = newArticle.Price,
                    Stock = newArticle.Stock,
                    Description = newArticle.Description,
                    CreatedAt = newArticle.CreatedAt,
                    UpdatedAt = newArticle.UpdatedAt
                };
            }
        }

        public List<ArticleDTO> Get()
        {
            using (var context = new AppDbContext())
            {
                var articles = context.Articles
                    .Select(a => new ArticleDTO
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Price = a.Price,
                        Description = a.Description,
                        Stock = a.Stock,
                        CreatedAt = a.CreatedAt,
                        UpdatedAt = a.UpdatedAt
                    })
                    .ToList();

                return articles;
            }
        }

        public Articles GetById(int articleId)
        {
            var article = dbContext.Articles.FirstOrDefault(x=> x.Id == articleId);
            return article;
        }

        public ArticleDTO Update(ArticleDTO article)
        {
            using (var context = new AppDbContext())
            {
                var existingArticle = context.Articles.Find(article.Id);
                if (existingArticle == null)
                {
                    throw new Exception("Article not found");
                }
                existingArticle.Name = article.Name;
                existingArticle.Price = article.Price;
                existingArticle.Description = article.Description;
                existingArticle.UpdatedAt = DateTime.UtcNow;
                context.SaveChanges();
                return new ArticleDTO
                {
                    Id = existingArticle.Id,
                    Name = existingArticle.Name,
                    Price = existingArticle.Price,
                    Description = existingArticle.Description,
                    CreatedAt = existingArticle.CreatedAt,
                    UpdatedAt = existingArticle.UpdatedAt
                };
            }
        }

        public bool Delete(int id)
        {
            using (var context = new AppDbContext())
            {
                var article = context.Articles.Find(id);
                if (article == null)
                {
                    return false; // Article not found
                }
                context.Articles.Remove(article);
                context.SaveChanges();
                return true; // Article deleted successfully
            }
        }
    }
}
