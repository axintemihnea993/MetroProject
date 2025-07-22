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

        public ArticlesController(AppDbContext context)
        {
            _repository = new ArticlesRepository(context);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ArticleDTO>> Get()
        {
            var articles = _repository.Get();
            return Ok(articles);
        }

        [HttpGet("{id}")]
        public ActionResult<ArticleDTO> GetById(int id)
        {
            var article = _repository.Get().Find(a => a.Id == id);
            if (article == null)
                return NotFound();
            return Ok(article);
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
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<ArticleDTO> Update(int id, [FromBody] ArticleDTO article)
        {
            if (id != article.Id)
                return BadRequest("ID mismatch.");

            try
            {
                var updated = _repository.Update(article);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _repository.Delete(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}