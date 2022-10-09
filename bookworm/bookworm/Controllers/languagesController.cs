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
    public class languagesController : ApiController
    {
        private BookwormModel db = new BookwormModel();

        // GET: api/languages
        public IQueryable<language> Getlanguages()
        {
            return db.languages;
        }

        // GET: api/languages/5
        [ResponseType(typeof(language))]
        public IHttpActionResult Getlanguage(int id)
        {
            language language = db.languages.Find(id);
            if (language == null)
            {
                return NotFound();
            }

            return Ok(language);
        }

        // PUT: api/languages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putlanguage(int id, language language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != language.lang_id)
            {
                return BadRequest();
            }

            db.Entry(language).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!languageExists(id))
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

        // POST: api/languages
        [ResponseType(typeof(language))]
        public IHttpActionResult Postlanguage(language language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.languages.Add(language);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = language.lang_id }, language);
        }

        // DELETE: api/languages/5
        [ResponseType(typeof(language))]
        public IHttpActionResult Deletelanguage(int id)
        {
            language language = db.languages.Find(id);
            if (language == null)
            {
                return NotFound();
            }

            db.languages.Remove(language);
            db.SaveChanges();

            return Ok(language);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool languageExists(int id)
        {
            return db.languages.Count(e => e.lang_id == id) > 0;
        }
    }
}