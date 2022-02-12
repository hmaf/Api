using EshopApi.Contract;
using EshopApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EshopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        [ResponseCache(Duration = 120  )]
        public IActionResult GetCustomer()
        {
            var res =  new ObjectResult(_customerRepository.GetList())
            {
                StatusCode = (int) HttpStatusCode.OK
            };
            
            Request.HttpContext.Response.Headers.Add("x_name", "Hadi Maftouhi");
            return res;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            if (await CustomerExists(id))
            {
                var res = await _customerRepository.GetById(id);
                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }

        private async Task<bool> CustomerExists(int id)
        {
            return await _customerRepository.IsExists(id);
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _customerRepository.Add(customer);
            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            await _customerRepository.Update(customer);
            return Ok(customer);
        }   

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            await _customerRepository.Delete(id);
            return Ok();
        }
    }
}
