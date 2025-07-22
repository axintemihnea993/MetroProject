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

        public PaymentsController(AppDbContext dbContext)
        {
            _repository = new PaymentsRepository(dbContext);
        }

        [HttpGet]
        public ActionResult<IEnumerable<PaymentDTO>> Get()
        {
            var payments = _repository.Get();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public ActionResult<PaymentDTO> GetById(int id)
        {
            var payment = _repository.Get().Find(p => p.Id == id);
            if (payment == null)
                return NotFound();
            return Ok(payment);
        }

        [HttpPost]
        public ActionResult<PaymentDTO> Create([FromBody] PaymentDTO payment)
        {
            try
            {
                var created = _repository.Create(payment);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<PaymentDTO> Update(int id, [FromBody] PaymentDTO payment)
        {
            if (id != payment.Id)
                return BadRequest("ID mismatch.");

            try
            {
                var updated = _repository.Update(payment);
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