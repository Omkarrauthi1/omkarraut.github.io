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
    public class rolesController : ApiController
    {
        private BookwormModel db = new BookwormModel();

        // GET: api/roles
        public IQueryable<role> Getroles()
        {
            return db.roles;
        }

        // GET: api/roles/5
        [ResponseType(typeof(role))]
        public IHttpActionResult Getrole(int id)
        {
            role role = db.roles.Find(id);
            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        // PUT: api/roles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putrole(int id, role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != role.id)
            {
                return BadRequest();
            }

            db.Entry(role).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!roleExists(id))
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

        // POST: api/roles
        [ResponseType(typeof(role))]
        public IHttpActionResult Postrole(role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.roles.Add(role);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (roleExists(role.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = role.id }, role);
        }

        // DELETE: api/roles/5
        [ResponseType(typeof(role))]
        public IHttpActionResult Deleterole(int id)
        {
            role role = db.roles.Find(id);
            if (role == null)
            {
                return NotFound();
            }

            db.roles.Remove(role);
            db.SaveChanges();

            return Ok(role);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool roleExists(int id)
        {
            return db.roles.Count(e => e.id == id) > 0;
        }
    }
}