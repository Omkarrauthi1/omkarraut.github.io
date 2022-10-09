using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using bookworm.Models;

namespace bookworm.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class product_benController : ApiController
    {
        private BookwormModel db = new BookwormModel();

        // GET: api/product_ben
        public IQueryable<product_ben> Getproduct_ben()
        {
            return db.product_ben;
        }

        // GET: api/product_ben/5
        [ResponseType(typeof(product_ben))]
        public IHttpActionResult Getproduct_ben(int id)
        {
            product_ben product_ben = db.product_ben.Find(id);
            if (product_ben == null)
            {
                return NotFound();
            }

            return Ok(product_ben);
        }

        // PUT: api/product_ben/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putproduct_ben(int id, product_ben product_ben)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product_ben.ProdBen_id)
            {
                return BadRequest();
            }

            db.Entry(product_ben).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!product_benExists(id))
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

        // POST: api/product_ben
        [ResponseType(typeof(product_ben))]
        public IHttpActionResult Postproduct_ben(product_ben product_ben)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.product_ben.Add(product_ben);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product_ben.ProdBen_id }, product_ben);
        }

        // DELETE: api/product_ben/5
        [ResponseType(typeof(product_ben))]
        public IHttpActionResult Deleteproduct_ben(int id)
        {
            product_ben product_ben = db.product_ben.Find(id);
            if (product_ben == null)
            {
                return NotFound();
            }

            db.product_ben.Remove(product_ben);
            db.SaveChanges();

            return Ok(product_ben);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool product_benExists(int id)
        {
            return db.product_ben.Count(e => e.ProdBen_id == id) > 0;
        }
    }
}