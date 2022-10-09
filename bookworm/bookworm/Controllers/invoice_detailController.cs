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
    public class invoice_detailController : ApiController
    {
        private BookwormModel db = new BookwormModel();

        // GET: api/invoice_detail
        public IQueryable<invoice_detail> Getinvoice_detail(int param)
        {
            var invoice_det = db.invoice_detail.Where(inv => inv.user_id==param).Include(elem => elem.product); ;
            Int32 ?max = invoice_det.Max(inv => inv.Invoice_Id);
            var result = db.invoice_detail.Where(inv => inv.Invoice_Id == max).Include(elem=>elem.product).Include(elem=>elem.invoice);
            return result;
        }
        
        //// GET: api/invoice_detail/5
        //[ResponseType(typeof(invoice_detail))]
        //public IHttpActionResult Getinvoice_detail(int id)
        //{
        //    invoice_detail invoice_detail = db.invoice_detail.Find(id);
        //    if (invoice_detail == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(invoice_detail);
        //}

        // PUT: api/invoice_detail/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putinvoice_detail(int id, invoice_detail invoice_detail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoice_detail.InvDtl_Id)
            {
                return BadRequest();
            }

            db.Entry(invoice_detail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!invoice_detailExists(id))
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

        // POST: api/invoice_detail
        [ResponseType(typeof(invoice_detail))]
        public IHttpActionResult Postinvoice_detail(invoice_detail invoice_detail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.invoice_detail.Add(invoice_detail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = invoice_detail.InvDtl_Id }, invoice_detail);
        }

        // DELETE: api/invoice_detail/5
        [ResponseType(typeof(invoice_detail))]
        public IHttpActionResult Deleteinvoice_detail(int id)
        {
            invoice_detail invoice_detail = db.invoice_detail.Find(id);
            if (invoice_detail == null)
            {
                return NotFound();
            }

            db.invoice_detail.Remove(invoice_detail);
            db.SaveChanges();

            return Ok(invoice_detail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool invoice_detailExists(int id)
        {
            return db.invoice_detail.Count(e => e.InvDtl_Id == id) > 0;
        }
    }
}