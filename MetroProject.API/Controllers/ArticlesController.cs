using Microsoft.AspNetCore.Mvc;
using MetroProject.Application.DTOs;
using MetroProject.Application.Repositories;
using System;
using System.Collections.Generic;
using MetroProject.Domain;

namespace MetroProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly ArticlesRepository _repository;
        private readonly ILogger<ArticlesController> _logger;

        public ArticlesController(AppDbContext context, ILogger<ArticlesController> logger)
        {
            _repository = new ArticlesRepository(context);
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ArticleDTO>> Get()
        {
            try
            {
                var articles = _repository.Get();

                return Ok(articles);
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while retrieving articles.";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, new
                {
                    Message = errorMessage,
                    Details = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ArticleDTO> GetById(int id)
        {
            try
            {
                var article = _repository.Get().Find(a => a.Id == id);

                if (article == null)
                    return NotFound();

                return Ok(article);
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred while retrieving article with ID {id}.";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, new
                {
                    Message = errorMessage,
                    Details = ex.Message
                });
            }
        }

        [HttpPost]
        public ActionResult<ArticleDTO> Create([FromBody] ArticleDTO article)
        {
            try
            {
                var created = _repository.Create(article);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while creating the article.";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, new
                {
                    Message = errorMessage,
                    Details = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<ArticleDTO> Update(int id, [FromBody] ArticleDTO article)
        {
            try
            {
                var updated = _repository.Update(article);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while updating the article.";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, new
                {
                    Message = errorMessage,
                    Details = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var result = _repository.Delete(id);

                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred while deleting the article with ID {id}.";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, new
                {
                    Message = errorMessage,
                    Details = ex.Message
                });
            }
        }
    }
}