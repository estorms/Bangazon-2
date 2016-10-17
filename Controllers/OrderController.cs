using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bangazon.Data;
using Bangazon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace WebAPIApplication.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")] //annotation: route matches name of the values. PFM. ANNOTATIONS ALSO KNOWN AS DECORATORS
    public class OrdersController : Controller
    {
        private BangazonContext context;

        public OrdersController(BangazonContext ctx)
        {
            context = ctx;
        }
      
        // GET api/values/5
        [HttpGet]
        public IActionResult Get()
        {
            //querying customer table in database context and selecting all customers, i.e., from the customer table, select everything.
            IQueryable<object> orders = from order in context.Order select order;

            if (orders == null)
            {
                return NotFound(); //produces a 404 response
            }

            return Ok(orders); //http response of status 200 and return to client

        }
        
        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Order order = context.Order.Single(m => m.OrderId == id);

                if (order == null)
                {
                    return NotFound();
                }
                
                return Ok(order);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }


        }

        // POST api/values
        [HttpPost]
        //FromBody means from the body of the request
        public IActionResult Post([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Order.Add(order);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.OrderId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetOrder", new { id = order.OrderId }, order);
        }

        // PUT api/values/5
      [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Order order)
        {
               if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (order == null)
            {
                return NotFound();
            }
            context.Order.Update(order);
            context.SaveChanges();

            return Ok(order);

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order order = context.Order.Single(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            
            context.Order.Remove(order);
            context.SaveChanges();

            return Ok();
        }

        private bool OrderExists(int id)
        {
            return context.Order.Count(e => e.OrderId == id) > 0;
        }
    }
}
