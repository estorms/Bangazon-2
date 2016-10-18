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
    public class LineItemsController : Controller
    {
        private BangazonContext context;

        public LineItemsController(BangazonContext ctx)
        {
            context = ctx;
        }
      
        // GET api/values/5
        [HttpGet]
        public IActionResult Get()
        {
            //querying customer table in database context and selecting all customers, i.e., from the customer table, select everything.
            IQueryable<object> lineitems = from lineitem in context.LineItem select lineitem;

            if (lineitems == null)
            {
                return NotFound(); //produces a 404 response
            }

            return Ok(lineitems); //http response of status 200 and return to client

        }
        
        [HttpGet("{id}", Name = "GetLineItem")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                LineItem lineitem = context.LineItem.Single(m => m.LineItemId == id);

                if (lineitem == null)
                {
                    return NotFound();
                }
                
                return Ok(lineitem);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }


        }

        // POST api/values
        [HttpPost]
        //FromBody means from the body of the request
        public IActionResult Post([FromBody] LineItem lineitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.LineItem.Add(lineitem);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LineItemExists(lineitem.LineItemId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetLineItem", new { id = lineitem.LineItemId }, lineitem);
        }

        // PUT api/values/5
      [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]LineItem lineitem)
        {
               if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (lineitem == null)
            {
                return NotFound();
            }
            context.LineItem.Update(lineitem);
            context.SaveChanges();

            return Ok(lineitem);

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LineItem lineitem = context.LineItem.Single(m => m.LineItemId == id);
            if (lineitem == null)
            {
                return NotFound();
            }
            
            context.LineItem.Remove(lineitem);
            context.SaveChanges();

            return Ok();
        }

        private bool LineItemExists(int id)
        {
            return context.LineItem.Count(e => e.LineItemId == id) > 0;
        }
    }
}
