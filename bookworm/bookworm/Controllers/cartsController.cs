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
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Configuration;

namespace bookworm.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class cartsController : ApiController
    {
        private BookwormModel db = new BookwormModel();
        // GET: api/carts
        public IQueryable<cart> Getfromcart()
        {
            return db.carts;
        }

        // GET: api/carts/userID
        [ResponseType(typeof(cart))]
        public IHttpActionResult Getcart(int param)
        {
            var cart = db.carts.Where(elem => elem.user_id == param).Include(elem => elem.product);
            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        // PUT: api/carts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcart(int param, cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (param != cart.Cart_id)
            {
                return BadRequest();
            }

            db.Entry(cart).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cartExists(param))
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

        //ForAddtoCart
        // POST: api/carts
        [ResponseType(typeof(cart))]
        public String Postcart(cart cart)
        {
            //db.carts.Include(elem=>elem.user_master).
            if (db.myshelves.Where(elem => elem.Customer_id == cart.user_id).Any(elem => elem.Product_id == cart.Product_id && elem.isActive.ToLower() == "y"))
            {
                return "You already have this product in Shelf!";
            }
            else if (db.carts.Where(elem => elem.user_id == cart.user_id).Any(elem => elem.Product_id == cart.Product_id))
            {
                return "You have already Added this product in Cart!";
            }
            //&& elem.Customer_id == cart.user_id);          

            db.carts.Add(cart);
            db.SaveChanges();

            return "Added to Cart Successfully!!";
        }




        //forDirectBUy
        [ResponseType(typeof(cart))]
        public String Postdirectbuy(cart cart)
        {
            //db.carts.Include(elem=>elem.user_master).
            if (db.myshelves.Where(elem => elem.Customer_id == cart.user_id).Any(elem => elem.Product_id == cart.Product_id && elem.isActive.ToLower() == "y"))
            {
                return "You have Already purchased this Book !!!";
            }

            var t = db.carts.Where(elem => elem.user_id == cart.user_id);
            foreach (var i in t)
            {
                i.is_selected = "N";
            }

            if (db.carts.Where(elem => elem.user_id == cart.user_id).Any(elem => elem.Product_id == cart.Product_id))
            {
                db.Entry(cart).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    Console.WriteLine("exeption occuerd!");
                }
                return "Added to Cart Successfully!!";
            }
            //&& elem.Customer_id == cart.user_id);          

            db.carts.Add(cart);
            db.SaveChanges();

            return "Added to Cart Successfully!!";
        }

        // DELETE: api/carts/5
        [ResponseType(typeof(cart))]
        public IHttpActionResult Deletecart(int param)
        {
            cart cart = db.carts.Find(param);
            if (cart == null)
            {
                return NotFound();
            }

            db.carts.Remove(cart);
            db.SaveChanges();

            return Ok(cart);
        }

        public String Gettotalamt(int param)
        {
            int amount;
            SqlConnection sqlCon = null;
            String SqlconString = ConfigurationManager.ConnectionStrings["BookwormModelConnection"].ConnectionString;
            
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_totalAmount", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@uid", SqlDbType.Int).Value = param;
                    sql_cmnd.Parameters.Add("@amt", SqlDbType.Int);
                    sql_cmnd.Parameters["@amt"].Direction = ParameterDirection.Output;
                    sql_cmnd.ExecuteNonQuery();
                    amount= (int)sql_cmnd.Parameters["@amt"].Value;
                    sqlCon.Close();
                }
            return amount.ToString();
        }

        [ResponseType(typeof(cart))]
        public IHttpActionResult Addselected(cart[] carts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var t in carts)
            {
                db.Entry(t).State = EntityState.Modified;
            }
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                Console.WriteLine("exception occured!");
            }
            return StatusCode(HttpStatusCode.NoContent);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool cartExists(int id)
        {
            return db.carts.Count(e => e.Cart_id == id) > 0;
        }
    }
}