using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using bookworm.Models;


namespace bookworm.Controllers
{
    [EnableCors(origins: "http://localhost:3000",headers:"*",methods:"*")]
    public class productsController : ApiController
    {
        private BookwormModel db = new BookwormModel();

        // GET: api/products/Getproducts
        public IQueryable<product> Getproducts()
        {
            return db.products;
        }

        // GET: api/products/5
        [ResponseType(typeof(product))]
        public IHttpActionResult Getproduct(int param)
        {
            product product = db.products.Find(param);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [ResponseType(typeof(product))]
        public IHttpActionResult Getproductbypub(int param)
        {
            var products = db.products.Where(elem => elem.product_publisher == param);
            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [ResponseType(typeof(product))]
        public IHttpActionResult GetproductByName(String param)
        {
            IQueryable<product> prods = from p in db.products where p.product_name.Contains(param) select p;
            if (prods == null)
            {
                return NotFound();
            }
            return Ok(prods);
        }

        [ResponseType(typeof(product))]
        public IHttpActionResult GetproductRentable()
        {
            IQueryable<product> prods = from p in db.products where p.is_rentable == true select p;
            //product product = db.products.Find(5);
            if (prods == null)
            {
                return NotFound();
            }

            return Ok(prods);
        }

        // PUT: api/products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putproduct(int param, product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (param != product.product_id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productExists(param))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/products
        [ResponseType(typeof(product))]
        public IHttpActionResult Postproduct(product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.products.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.product_id }, product);
        }

        // DELETE: api/products/5
        [ResponseType(typeof(product))]
        public IHttpActionResult Deleteproduct(int id)
        {
            product product = db.products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool productExists(int id)
        {
            return db.products.Count(e => e.product_id == id) > 0;
        }
    }
}