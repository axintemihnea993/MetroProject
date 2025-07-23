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
    public class CustomersController : ControllerBase
    {
        private readonly CustomersRepository _repository;

        public CustomersController(AppDbContext context)
        {
            _repository = new CustomersRepository(context);
        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerDTO>> Get()
        {
            var customers = _repository.Get();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerDTO> GetById(int id)
        {
            var customer = _repository.Get().Find(c => c.Id == id);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public ActionResult<CustomerDTO> Create([FromBody] CustomerDTO customer)
        {
            try
            {
                var created = _repository.Create(customer);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<CustomerDTO> Update(int id, [FromBody] CustomerDTO customer)
        {
            if (id != customer.Id)
                return BadRequest("ID mismatch.");

            try
            {
                var updated = _repository.Update(customer);
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