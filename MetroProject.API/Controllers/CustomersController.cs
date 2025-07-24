using Microsoft.AspNetCore.Mvc;
using MetroProject.Application.DTOs;
using MetroProject.Application.Repositories;
using System;
using System.Collections.Generic;
using MetroProject.Domain;
using MetroProject.Domain.Interface;

namespace MetroProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IRepository<CustomerDTO> _repository;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ILogger<CustomersController> logger, IRepository<CustomerDTO> repository)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerDTO>> Get()
        {
            try
            {
                var customers = _repository.Get();

                return Ok(customers);
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while retrieving customers.";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, new
                {
                    Message = errorMessage,
                    Details = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerDTO> GetById(int id)
        {
            try
            {
                var customer = _repository.Get().Find(c => c.Id == id);

                if (customer == null)
                    return NotFound();

                return Ok(customer);
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred while retrieving customer with ID {id}.";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, new
                {
                    Message = errorMessage,
                    Details = ex.Message
                });
            }
        }

        [HttpPost]
        public ActionResult<CustomerDTO> Create([FromBody] CustomerDTO customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var created = _repository.Create(customer);

                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred while creating customer.";
                _logger.LogError(ex, errorMessage);
                return StatusCode(500, new
                {
                    Message = errorMessage,
                    Details = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<CustomerDTO> Update([FromBody] CustomerDTO customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updated = _repository.Update(customer);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred while updating customer.";
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
                var errorMessage = $"An error occurred while deleting customer with ID {id}.";
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