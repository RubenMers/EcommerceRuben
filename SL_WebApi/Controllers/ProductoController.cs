using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebApi.Controllers
{
    public class ProductoController : ApiController
    {
        [HttpGet]
        [Route("api/Producto")]
        // GET: api/Producto
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Producto.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api/Producto/{IdProducto}")]
        // GET: api/Producto/5
        public IHttpActionResult GetById(int IdProducto)
        {
            ML.Result result = BL.Producto.GetIdProducto(IdProducto);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        [Route("api/Producto")]
        // POST: api/Producto
        public IHttpActionResult Post([FromBody]ML.Producto producto)
        {
            ML.Result result = BL.Producto.AddImagen(producto);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/Producto/Put")]
        // PUT: api/Producto/5
        public IHttpActionResult Put([FromBody]ML.Producto producto)
        {
            var result = BL.Producto.UpdateEF(producto);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api/Producto/Delete/{IdProducto}")]
        // DELETE: api/Producto/5
        public IHttpActionResult Delete(int IdProducto)
        {
            ML.Producto producto = new ML.Producto();
            producto.IdProducto = IdProducto;
            var result = BL.Producto.DeleteEF(producto);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
