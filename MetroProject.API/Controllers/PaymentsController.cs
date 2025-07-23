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
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentsRepository _repository;
        private readonly ILogger<PaymentsController> logger;

        public PaymentsController(AppDbContext dbContext, ILogger<PaymentsController> logger)
        {
            _repository = new PaymentsRepository(dbContext);
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PaymentDTO>> Get()
        {
            try
            {
                var payments = _repository.Get();
                return Ok(payments);
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while retrieving payments.";
                logger.LogError(ex, errorMessage);
                return StatusCode(500, new
                {
                    Message = errorMessage,
                    Details = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<PaymentDTO> GetById(int id)
        {
            try
            {

            var payment = _repository.Get().Find(p => p.Id == id);
            if (payment == null)
                return NotFound();
            return Ok(payment);
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred while retrieving payment with ID {id}.";
                logger.LogError(ex, errorMessage);
                return StatusCode(500, new
                {
                    Message = errorMessage,
                    Details = ex.Message
                });
            }
        }

        [HttpPost]
        public ActionResult<PaymentDTO> Create([FromBody] PaymentDTO payment)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var created = _repository.Create(payment);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred while creating payment.";
                logger.LogError(ex, errorMessage);
                return StatusCode(500, new
                {
                    Message = errorMessage,
                    Details = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<PaymentDTO> Update(int id, [FromBody] PaymentDTO payment)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updated = _repository.Update(payment);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred while updating payment.";
                logger.LogError(ex, errorMessage);
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
                var errorMessage = $"An error occurred while deleting payment with ID {id}.";
                logger.LogError(ex, errorMessage);
                return StatusCode(500, new
                {
                    Message = errorMessage,
                    Details = ex.Message
                });
            }
        }
    }
}