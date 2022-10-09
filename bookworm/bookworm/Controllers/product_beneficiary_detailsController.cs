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
    public class product_beneficiary_detailsController : ApiController
    {
        private BookwormModel db = new BookwormModel();

        // GET: api/product_beneficiary_details
        public IQueryable<product_beneficiary_details> Getproduct_beneficiary_details()
        {
            return db.product_beneficiary_details;
        }

        // GET: api/product_beneficiary_details/5
        [ResponseType(typeof(product_beneficiary_details))]
        public IHttpActionResult Getproduct_beneficiary_details(int param)
        {
            product_beneficiary_details product_beneficiary_details = db.product_beneficiary_details.Find(param);
            if (product_beneficiary_details == null)
            {
                return NotFound();
            }

            return Ok(product_beneficiary_details);
        }

        // PUT: api/product_beneficiary_details/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putproduct_beneficiary_details(int param, product_beneficiary_details product_beneficiary_details)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (param != product_beneficiary_details.Ben_id)
            {
                return BadRequest();
            }

            db.Entry(product_beneficiary_details).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!product_beneficiary_detailsExists(param))
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


        // POST: api/product_beneficiary_details
        [HttpPost]
        [ResponseType(typeof(product_beneficiary_details))]
        public IHttpActionResult Postproduct_beneficiary_details(product_beneficiary_details product_beneficiary_details)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.product_beneficiary_details.Add(product_beneficiary_details);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product_beneficiary_details.Ben_id }, product_beneficiary_details);
        }

        // DELETE: api/product_beneficiary_details/5
        [ResponseType(typeof(product_beneficiary_details))]
        public IHttpActionResult Deleteproduct_beneficiary_details(int id)
        {
            product_beneficiary_details product_beneficiary_details = db.product_beneficiary_details.Find(id);
            if (product_beneficiary_details == null)
            {
                return NotFound();
            }

            db.product_beneficiary_details.Remove(product_beneficiary_details);
            db.SaveChanges();

            return Ok(product_beneficiary_details);
        }

        [HttpGet]
        public String Checkemail(String param)
        {
            string param2 = param + ".com";
            Console.WriteLine(param);
            if (db.product_beneficiary_details.Any(elem => elem.Ben_email_id == param2))
                return "The Email has already been taken.";
            return "";
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool product_beneficiary_detailsExists(int id)
        {
            return db.product_beneficiary_details.Count(e => e.Ben_id == id) > 0;
        }
    }
}