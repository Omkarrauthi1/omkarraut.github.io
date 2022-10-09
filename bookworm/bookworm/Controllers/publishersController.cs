using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using bookworm.Models;

namespace bookworm.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class publishersController : ApiController
    {
        private BookwormModel db = new BookwormModel();

        // GET: api/publishers
        public IQueryable<publisher> Getpublishers()
        {
            return db.publishers;
        }

        // GET: api/publishers/5
        [ResponseType(typeof(publisher))]
        public async Task<IHttpActionResult> Getpublisher(int id)
        {
            publisher publisher = await db.publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            return Ok(publisher);
        }

        // GET: api/publishers/GetEmail
        [ResponseType(typeof(publisher))]
        public string GetEmail(String param)
        {
            string b = param + ".com";

            if (db.publishers.Any(elem => elem.Ben_email_id == b))
                return "The Email has already been taken.";
            return "";


        }

        // GET: api/publishers/GetUserName
        [ResponseType(typeof(publisher))]
        public string GetUserName(String param)
        {
            if (db.publishers.Any(elem => elem.Ben_user_name == param))
                return "The Username has already been taken.";
            return "";
        }

        // PUT: api/publishers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putpublisher(int id, publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != publisher.Ben_id)
            {
                return BadRequest();
            }

            db.Entry(publisher).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!publisherExists(id))
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

        // POST: api/publishers/PostNewUser
        [ResponseType(typeof(publisher))]
        public async Task<IHttpActionResult> PostNewUser(publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.publishers.Add(publisher);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { param = publisher.Ben_id }, publisher);
        }
        // POST: api/publisher/PostLogin
        [ResponseType(typeof(publisher))]
        public async Task<IHttpActionResult> PostLogin(publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = from user in db.publishers where publisher.Ben_user_name == user.Ben_user_name 
                         && publisher.Ben_password == user.Ben_password select user;

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        

        // DELETE: api/publishers/5
        [ResponseType(typeof(publisher))]
        public async Task<IHttpActionResult> Deletepublisher(int id)
        {
            publisher publisher = await db.publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            db.publishers.Remove(publisher);
            await db.SaveChangesAsync();

            return Ok(publisher);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool publisherExists(int id)
        {
            return db.publishers.Count(e => e.Ben_id == id) > 0;
        }
    }
}