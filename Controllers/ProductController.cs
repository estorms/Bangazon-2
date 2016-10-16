using System;
using System.Collections.Generic;
using System.Linq;
//I THINK that System.Linq is a method of making SQL queries using C#, DbContext has built-in access to System.Linq, which is why you don't have to pass System.Linq into BangazonContext ... provides a C#-specific syntax for making SQL queries.
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
    public class ProductsController : Controller
    {
        //Here we create a private property of type BangazonContext class (remember, BangazonContext inherits from DbContext) and call it context. Context is kept private, but it's being passed into the public methods defined in the ProductsController class every time the ProductsController custom constructor is being called. The context property, of type BangazonContext, is accessible every time an instance of ProductsController is created. 
        private BangazonContext context;


        public ProductsController(BangazonContext ctx)
        {
            context = ctx;
        }
      
      
        // GET api/values/5
        [HttpGet] //NEED TO BETTER UNDERSTAND WHAT THESE ANNOTATIONS DO!


        public IActionResult Get()
        {
            //querying customer table in database context and selecting all customers, i.e., from the customer table, select everything. IQueryable relies on System.Linq
            IQueryable<object> products = from product in context.Product select product;

            if (products == null)
            {
                return NotFound(); //produces a 404 response
            }

            return Ok(products); //http response of status 200 and return to client

        }
        
        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Product product = context.Product.Single(m => m.ProductId == id);

                if (product == null)
                {
                    return NotFound();
                }
                
                return Ok(product);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }


        }

        // POST api/values
        [HttpPost]
        //FromBody means from the body of the request
        public IActionResult Post([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Product.Add(product);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.ProductId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetProduct", new { id = product.ProductId }, product);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private bool ProductExists(int id)
        {
            return context.Product.Count(e => e.ProductId == id) > 0;
        }
    }
}
