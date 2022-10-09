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
    [EnableCors(origins: "http://localhost:3000", headers: "", methods: "")]
    public class myshelvesController : ApiController
    {
        private BookwormModel db = new BookwormModel();

        // GET: api/myshelves
        [ResponseType(typeof(myshelf))]
        [HttpGet]
        public IQueryable<myshelf> Getmyshelves()
        {
            Console.WriteLine("inside");
            return db.myshelves;
        }

        // GET: api/myshelves/Getmyshelf/customerId
        [ResponseType(typeof(myshelf))]
        public IHttpActionResult Getmyshelf(int param)
        {
            var myshelf = db.myshelves.Where(elem => elem.Customer_id == param && elem.isActive.ToLower() == "y").Include(elem => elem.product);
            if (myshelf == null)
            {
                return NotFound();
            }

            return Ok(myshelf);
        }

        // PUT: api/myshelves/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putmyshelf(int param, myshelf myshelf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (param != myshelf.Shelf_id)
            {
                return BadRequest();
            }

            db.Entry(myshelf).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!myshelfExists(param))
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

        // POST: api/myshelves
        [ResponseType(typeof(myshelf))]
        public IHttpActionResult Postmyshelf(myshelf myshelf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.myshelves.Add(myshelf);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = myshelf.Shelf_id }, myshelf);
        }

        // DELETE: api/myshelves/5
        [ResponseType(typeof(myshelf))]
        public IHttpActionResult Deletemyshelf(int param)
        {
            myshelf myshelf = db.myshelves.Find(param);
            if (myshelf == null)
            {
                return NotFound();
            }

            db.myshelves.Remove(myshelf);
            db.SaveChanges();

            return Ok(myshelf);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool myshelfExists(int id)
        {
            return db.myshelves.Count(e => e.Shelf_id == id) > 0;
        }
    }
}