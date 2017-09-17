using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CustomerAppBLL;
using CustomerAppBLL.BusinessObjects;

namespace CustomerRestAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        BLLFacade facade = new BLLFacade();
        // GET: api/Customers
        [HttpGet]
        public IEnumerable<CustomerBO> Get()
        {
            return facade.CustomerService.GetAll();
        }

        // GET: api/Customers/5
        [HttpGet("{id}", Name = "Get")]
        public CustomerBO Get(int id)
        {
            return facade.CustomerService.Get(id);
        }
        
        // POST: api/Customers
        [HttpPost]
        public IActionResult Post([FromBody]CustomerBO cust)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(facade.CustomerService.Create(cust));
        }
        
        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CustomerBO cust)
        {
            if (id != cust.Id)
            {
                return StatusCode(405, ("Oh no! Path Id does not match Customer ID in json object"));
            }
            try
            {
                return Ok(facade.CustomerService.Update(cust));
            }
            catch(InvalidOperationException e)
            {
                return StatusCode(404, (e.Message));
            }
           
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            facade.CustomerService.Delete(id);
        }
    }
}
