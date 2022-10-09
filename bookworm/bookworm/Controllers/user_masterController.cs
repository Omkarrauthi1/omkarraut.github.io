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
    public class user_masterController : ApiController
    {
        private BookwormModel db = new BookwormModel();

        // GET: api/user_master
        public IQueryable<user_master> Getuser_master()
        {
            return db.user_master;
        }

        // GET: api/user_master/5
        [ResponseType(typeof(user_master))]
        public async Task<IHttpActionResult> Getuser_master(int param)
        {
            user_master user_master = await db.user_master.FindAsync(param);
            if (user_master == null)
            {
                return NotFound();
            }

            return Ok(user_master);
        }
        // GET: api/user_master/GetEmail
        [ResponseType(typeof(user_master))]
        public string GetEmail(String param)
        {
            string b = param + ".com";
            
            if (db.user_master.Any(elem => elem.email_id == b))
                return "The Email has already been taken.";
            return "";


        }

        // GET: api/user_master/GetUserName
        [ResponseType(typeof(user_master))]
        public string GetUserName(String param)
        {
            if (db.user_master.Any(elem => elem.user_name == param))
                return "The Username has already been taken.";
            return "";
        }

        // PUT: api/user_master/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putuser_master(int param, user_master user_master)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (param != user_master.user_id)
            {
                return BadRequest();
            }

            db.Entry(user_master).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!user_masterExists(param))
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

        // POST: api/user_master/PostNewUser
        [ResponseType(typeof(user_master))]
        public async Task<IHttpActionResult> PostNewUser(user_master user_master)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.user_master.Add(user_master);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { param = user_master.user_id }, user_master);
        }

        // POST: api/user_master/PostLogin
        [ResponseType(typeof(user_master))]
        public async Task<IHttpActionResult> PostLogin(user_master user_master)
        {
           // user_master.password=((int)(user_master.password))/12)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IQueryable<user_master> result;
            try
            {
                 result = from user in db.user_master where user.user_name ==
                          user_master.user_name && user.password == user_master.password select user;
            }
            catch
            {

                return NotFound();
            }
            return Ok(result);
        }

        // DELETE: api/user_master/5
        [ResponseType(typeof(user_master))]
        public async Task<IHttpActionResult> Deleteuser_master(int id)
        {
            user_master user_master = await db.user_master.FindAsync(id);
            if (user_master == null)
            {
                return NotFound();
            }

            db.user_master.Remove(user_master);
            await db.SaveChangesAsync();

            return Ok(user_master);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool user_masterExists(int id)
        {
            return db.user_master.Count(e => e.user_id == id) > 0;
        }
    }
}