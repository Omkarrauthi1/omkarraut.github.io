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
    public class generesController : ApiController
    {
        private BookwormModel db = new BookwormModel();

        // GET: api/generes
        public IQueryable<genere> Getgeneres()
        {
            return db.generes;
        }

        // GET: api/generes/5
        [ResponseType(typeof(genere))]
        public IHttpActionResult Getgenere(int id)
        {
            genere genere = db.generes.Find(id);
            if (genere == null)
            {
                return NotFound();
            }

            return Ok(genere);
        }

        // PUT: api/generes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putgenere(int id, genere genere)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != genere.genere_id)
            {
                return BadRequest();
            }

            db.Entry(genere).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!genereExists(id))
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

        // POST: api/generes
        [ResponseType(typeof(genere))]
        public IHttpActionResult Postgenere(genere genere)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.generes.Add(genere);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = genere.genere_id }, genere);
        }

        // DELETE: api/generes/5
        [ResponseType(typeof(genere))]
        public IHttpActionResult Deletegenere(int id)
        {
            genere genere = db.generes.Find(id);
            if (genere == null)
            {
                return NotFound();
            }

            db.generes.Remove(genere);
            db.SaveChanges();

            return Ok(genere);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool genereExists(int id)
        {
            return db.generes.Count(e => e.genere_id == id) > 0;
        }
    }
}