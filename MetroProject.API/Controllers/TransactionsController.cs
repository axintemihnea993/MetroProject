using MetroProject.Application.DTOs;
using MetroProject.Application.Features;
using MetroProject.Domain;
using MetroProject.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MetroProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public TransactionsController(AppDbContext context)
        {
            dbContext = context;
        }
        // POST api/<TransactionsController>
        [HttpPost]
        public ActionResult<CheckoutCommandDTO> Post([FromBody] CheckoutCommandDTO transaction)
        {
            var transactionRepository = new TransactionWithPaymentsRepository(dbContext);
            var createdTransaction = transactionRepository.Create(transaction);

            return Ok(createdTransaction);
        }
    }
}
