using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Bangazon.Data;
using Bangazon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace WebAPIApplication.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")] //annotation: route matches name of the values. PFM. ANNOTATIONS ALSO KNOWN AS DECORATORS
    public class CustomersController : Controller
    {
        private BangazonContext context;

        public CustomersController(BangazonContext ctx)
        {
            context = ctx;
        }

        // GET api/values/5
        [HttpGet]
        public IActionResult Get()
        {
            //querying customer table in database context and selecting all customers, i.e., from the customer table, select everything.
            IQueryable<object> customers = from customer in context.Customer select customer;

            if (customers == null)
            {
                return NotFound(); //produces a 404 response
            }

            return Ok(customers); //http response of status 200 and return to client

        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Customer customer = context.Customer.Single(m => m.CustomerId == id);

                if (customer == null)
                {
                    return NotFound();
                }

                return Ok(customer);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }


        }

        // POST api/values
        [HttpPost]
        //FromBody means from the body of the request
        public IActionResult Post([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Customer.Add(customer);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Customer customer)
        {
               if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (customer == null)
            {
                return NotFound();
            }
            context.Customer.Update(customer);
            context.SaveChanges();

            return Ok(customer);

        }


   

        // DELETE api/values/5

    [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Customer customer = context.Customer.Single(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            
            context.Customer.Remove(customer);
            context.SaveChanges();

            return Ok();
        }

        private bool CustomerExists(int id)
        {
            return context.Customer.Count(e => e.CustomerId == id) > 0;
        }
    }
}
