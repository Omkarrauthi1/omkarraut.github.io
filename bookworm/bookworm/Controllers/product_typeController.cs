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

namespace Bookworm.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class product_typeController : ApiController
    {
        private BookwormModel db = new BookwormModel();

        // GET: api/product_type
        public IQueryable<product_type> Getproduct_type()
        {
            return db.product_type;
        }

        // GET: api/product_type/5
        [ResponseType(typeof(product_type))]
        public IHttpActionResult Getproduct_type(int id)
        {
            product_type product_type = db.product_type.Find(id);
            if (product_type == null)
            {
                return NotFound();
            }

            return Ok(product_type);
        }

        // PUT: api/product_type/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putproduct_type(int id, product_type product_type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product_type.type_id)
            {
                return BadRequest();
            }

            db.Entry(product_type).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!product_typeExists(id))
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

        // POST: api/product_type
        [ResponseType(typeof(product_type))]
        public IHttpActionResult Postproduct_type(product_type product_type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.product_type.Add(product_type);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product_type.type_id }, product_type);
        }

        // DELETE: api/product_type/5
        [ResponseType(typeof(product_type))]
        public IHttpActionResult Deleteproduct_type(int id)
        {
            product_type product_type = db.product_type.Find(id);
            if (product_type == null)
            {
                return NotFound();
            }

            db.product_type.Remove(product_type);
            db.SaveChanges();

            return Ok(product_type);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool product_typeExists(int id)
        {
            return db.product_type.Count(e => e.type_id == id) > 0;
        }
    }
}