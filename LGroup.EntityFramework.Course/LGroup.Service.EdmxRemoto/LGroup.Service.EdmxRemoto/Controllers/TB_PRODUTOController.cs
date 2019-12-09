using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using LGroup.Service.EdmxRemoto.DataAccess;


//uma Web API eh um serviço (DLL REMOTA). Eh um componente de integração
//dentro da Web Api (HTTP) estamos utilizando o protocolo OData. Eh um protocolo da Microsoft
//Ele permite tramsmitir, acessar um Entity Framework REMOTAMENTE um EF que está na nuvem
namespace LGroup.Service.EdmxRemoto.Controllers
{
    public class TB_PRODUTOController : ODataController
    {
        private FLUENTEntities db = new FLUENTEntities();

        // GET: odata/TB_PRODUTO
        [EnableQuery]
        public IQueryable<TB_PRODUTO> GetTB_PRODUTO()
        {
            return db.TB_PRODUTO;
        }

        // GET: odata/TB_PRODUTO(5)
        [EnableQuery]
        public SingleResult<TB_PRODUTO> GetTB_PRODUTO([FromODataUri] int key)
        {
            return SingleResult.Create(db.TB_PRODUTO.Where(tB_PRODUTO => tB_PRODUTO.ID_PRODUTO == key));
        }

        // PUT: odata/TB_PRODUTO(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<TB_PRODUTO> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TB_PRODUTO tB_PRODUTO = db.TB_PRODUTO.Find(key);
            if (tB_PRODUTO == null)
            {
                return NotFound();
            }

            patch.Put(tB_PRODUTO);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TB_PRODUTOExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(tB_PRODUTO);
        }

        // POST: odata/TB_PRODUTO
        public IHttpActionResult Post(TB_PRODUTO tB_PRODUTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TB_PRODUTO.Add(tB_PRODUTO);
            db.SaveChanges();

            return Created(tB_PRODUTO);
        }

        // PATCH: odata/TB_PRODUTO(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<TB_PRODUTO> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TB_PRODUTO tB_PRODUTO = db.TB_PRODUTO.Find(key);
            if (tB_PRODUTO == null)
            {
                return NotFound();
            }

            patch.Patch(tB_PRODUTO);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TB_PRODUTOExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(tB_PRODUTO);
        }

        // DELETE: odata/TB_PRODUTO(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            TB_PRODUTO tB_PRODUTO = db.TB_PRODUTO.Find(key);
            if (tB_PRODUTO == null)
            {
                return NotFound();
            }

            db.TB_PRODUTO.Remove(tB_PRODUTO);
            db.SaveChanges();

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

        private bool TB_PRODUTOExists(int key)
        {
            return db.TB_PRODUTO.Count(e => e.ID_PRODUTO == key) > 0;
        }
    }
}
