using MetroProject.Application.DTOs;
using MetroProject.Application.Repositories;
using MetroProject.Domain;
using MetroProject.Domain.DTOs;
using MetroProject.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MetroProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        // Change the type of transactionsRepository to IRepository<CheckoutCommandDTO>
        private readonly IRepository<CheckoutCommandDTO> transactionsRepository;
        private readonly ILogger<TransactionsController> logger;

        // Update the constructor parameter and assignment accordingly
        public TransactionsController(ILogger<TransactionsController> logger, IRepository<CheckoutCommandDTO> repository)
        {
            this.transactionsRepository = repository;
            this.logger = logger;
        }

        // POST api/<TransactionsController>
        [HttpPost]
        public ActionResult<CheckoutCommandDTO> Post([FromBody] CheckoutCommandDTO transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdTransaction = this.transactionsRepository.Create(transaction);

                return Ok(createdTransaction);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating transaction.");
                return StatusCode(500, new
                {
                    Message = "An internal server error occurred while creating transaction.",
                    Details = ex.Message
                });
            }
        }
    }
}
