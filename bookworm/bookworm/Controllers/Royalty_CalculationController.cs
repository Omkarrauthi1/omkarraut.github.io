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
    public class Royalty_CalculationController : ApiController
    {
        public class ViewModel
        {
            public int? roycal_qty { get; set; }
            public double? RoyaltyOnBasePrice { get; set; }
            public string product_name { get; set; }
        }
        public class ResultLine
        {
            public int? roycal_qty { get; set; }
            public double? RoyaltyOnBasePrice { get; set; }
            public string product_name { get; set; }
        }

        private BookwormModel db = new BookwormModel();

        // GET: api/Royalty_Calculation/GetPubRoyalty/1
        public IQueryable<ResultLine> GetPubRoyalty(int param)
        {

            var result = from Royalty in db.Royalty_Calculation
                         join Product in db.products on Royalty.Product_Id equals Product.product_id
                         where Royalty.Ben_Id == param
            select new ViewModel
            {
                product_name = Product.product_name,
                roycal_qty = Royalty.roycal_qty,
                RoyaltyOnBasePrice = Royalty.RoyaltyOnBasePrice };

            var result2=result.GroupBy(l => l.product_name)
    .Select(cl => new ResultLine
    {
        product_name =cl.FirstOrDefault<ViewModel>().product_name,
        roycal_qty = cl.Count(),
        RoyaltyOnBasePrice = cl.Sum(c => c.RoyaltyOnBasePrice)
        
    });
            return result2;
        }

        // GET: api/Royalty_Calculation/GetPubRoyaltyByTran/1
        public IQueryable<ResultLine> GetPubRoyaltyByTran(int param)
        {
            

            var result = from Royalty in db.Royalty_Calculation
                         join Product in db.products on Royalty.Product_Id equals Product.product_id
                         where Royalty.Ben_Id == param && Royalty.trantype=="r"
                         select new ViewModel
                         {
                             product_name = Product.product_name,
                             roycal_qty = Royalty.roycal_qty,
                             RoyaltyOnBasePrice = Royalty.RoyaltyOnBasePrice
                         };

     var result2 = result.GroupBy(l => l.product_name)
     .Select(cl => new ResultLine
     {
         product_name = cl.FirstOrDefault<ViewModel>().product_name,
         roycal_qty = cl.Count(),
         RoyaltyOnBasePrice = cl.Sum(c => c.RoyaltyOnBasePrice)

     });
            return result2;
        }

        // GET: api/Royalty_Calculation/5
        [ResponseType(typeof(Royalty_Calculation))]
        public async Task<IHttpActionResult> GetRoyalty_Calculation(int id)
        {
            Royalty_Calculation royalty_Calculation = await db.Royalty_Calculation.FindAsync(id);
            if (royalty_Calculation == null)
            {
                return NotFound();
            }

            return Ok(royalty_Calculation);
        }

        // PUT: api/Royalty_Calculation/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRoyalty_Calculation(int id, Royalty_Calculation royalty_Calculation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != royalty_Calculation.roycal_id)
            {
                return BadRequest();
            }

            db.Entry(royalty_Calculation).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Royalty_CalculationExists(id))
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

        // POST: api/Royalty_Calculation
        [ResponseType(typeof(Royalty_Calculation))]
        public async Task<IHttpActionResult> PostRoyalty_Calculation(Royalty_Calculation royalty_Calculation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Royalty_Calculation.Add(royalty_Calculation);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = royalty_Calculation.roycal_id }, royalty_Calculation);
        }

        // DELETE: api/Royalty_Calculation/5
        [ResponseType(typeof(Royalty_Calculation))]
        public async Task<IHttpActionResult> DeleteRoyalty_Calculation(int id)
        {
            Royalty_Calculation royalty_Calculation = await db.Royalty_Calculation.FindAsync(id);
            if (royalty_Calculation == null)
            {
                return NotFound();
            }

            db.Royalty_Calculation.Remove(royalty_Calculation);
            await db.SaveChangesAsync();

            return Ok(royalty_Calculation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Royalty_CalculationExists(int id)
        {
            return db.Royalty_Calculation.Count(e => e.roycal_id == id) > 0;
        }
    }
}