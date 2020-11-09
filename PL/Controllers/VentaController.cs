using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class VentaController : Controller
    {
        // GET: Venta
        public ActionResult ProductoGetAll()
        {
            ML.Result result = BL.Producto.Get();

            ML.Producto producto = new ML.Producto();

            producto.Productos = result.Objects;

            return View(producto);

        }
        [HttpGet]
        public ActionResult AddCarrito(int IdProducto)
        {
            ML.Producto producto = new ML.Producto();
            ML.DetalleVenta detalle = new ML.DetalleVenta();

            if (Session["Carrito"] == null)
            {
                detalle.Producto = new ML.Producto();
                detalle.Producto.IdProducto = IdProducto;
                
                //detalle.Producto = new ML.Producto();
                //producto.IdProducto = IdProducto;
                var result = BL.Producto.GetByIdEF(detalle.Producto);

                result.Objects = new List<Object>();
                result.Objects.Add(result.Object);
                Session["Carrito"] = result.Objects;

                return View("AddCarrito", result);
            }
            else
            {
                //producto.IdProducto = IdProducto.Value;
                detalle.Producto = new ML.Producto();
                detalle.Producto.IdProducto = IdProducto;
                var result = BL.Producto.GetByIdEF(detalle.Producto);

                result.Objects = (List<Object>)Session["Carrito"];

                int pos = 0;
                bool comprobar = false;

                foreach (ML.Producto productos in result.Objects.ToList())
                {
                    if (productos.IdProducto == IdProducto)
                    {
                        comprobar = true;
                        pos = productos.IdProducto;
                    }
                    else
                    {
                        comprobar = false;
                    }
                }


                if (comprobar == true)
                {
                    foreach (ML.Producto productos in result.Objects.ToList())
                    {
                        if (producto.IdProducto == pos)
                        {
                            detalle.Cantidad++;

                            detalle.Venta.Total = (detalle.Venta.Total +(Int32)(detalle.Cantidad * productos.Precio));
                            //detalle.Venta.Total = Convert.ToInt32((detalle.Cantidad * detalle.Producto.Precio));
                            break;
                        }
                    }
                }
                else
                {
                    result.Objects.Add(result.Object);
                    Session["Carrito"] = result.Objects;
                }

                return View("AddCarrito", result);

            }
        }

        public ActionResult IncrementProduct(int IdProducto)
        {
            //ML.DetalleVenta detalle = new ML.DetalleVenta();
            ML.Result result = new ML.Result();
            result.Objects = new List<Object>();
            result.Objects = (List<Object>)Session["Carrito"];

            foreach (ML.Producto producto in result.Objects.ToList())
            {
                if (producto.IdProducto == IdProducto)
                {
                    
                    
                    producto.DetalleVenta.Cantidad++;
                    producto.DetalleVenta.Venta.Total = Convert.ToInt32(producto.DetalleVenta.Cantidad * producto.Precio);
                    //producto.DetallePedido.Total = producto.DetallePedido.Cantidad * producto.Precio;
                    break;
                }
            }

            return View("AddCarrito", result);

        }

        public ActionResult DecrementProduct(int IdProducto)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<Object>();
            result.Objects = (List<Object>)Session["Carrito"];

            int pos = 0;
            bool comprobar = false;
            foreach (ML.Producto producto in result.Objects.ToList())
            {
                pos++;
                if (producto.IdProducto == IdProducto)
                {
                    producto.DetalleVenta.Cantidad--;
                    producto.DetalleVenta.Venta.Total = Convert.ToInt32((producto.DetalleVenta.Cantidad) * producto.Precio);
                    
                    comprobar = false;
                    if (producto.DetalleVenta.Cantidad <= 0)
                    {
                        comprobar = true;
                    }
                    break;
                }

            }

            if (comprobar == true)
            {
                result.Objects.RemoveAt(pos - 1);
            }

            return View("AddCarrito", result);

        }

        public ActionResult DeleteProduct(int IdProducto)
        {

            TempData["alertMessage"] = "Producto Eliminado";
            ML.Result result = new ML.Result();
            result.Objects = new List<Object>();
            result.Objects = (List<Object>)Session["Carrito"];
            int pos = 0;

            if (result.Objects.Count == 0)
            {
                TempData["alertMessage"] = "No Existen Productos Agrega uno";
                return View("GetAll", result);
            }
            else
            {
                foreach (ML.Producto producto in result.Objects.ToList())
                {
                    pos++;
                    if (producto.IdProducto == IdProducto)
                    {
                        break;
                    }
                }

                result.Objects.RemoveAt(pos - 1);

                return View("AddCarrito", result);
            }

        }

        /*public ActionResult ListProduct(List<ML.Producto> producto)
        {
            ML.Producto productos = new ML.Producto();


            if (Session["Carrito"] == null)
            {
                var result = new ML.Result();
                result.Objects = new List<Object>();
                //Existe
                foreach (ML.Producto productoItem in producto)
                {

                    if (productoItem.Selected == true)
                    {
                        productos.IdProducto = productoItem.IdProducto;
                        var consult = BL.Producto.GetByIdEF(productos);


                        result.Objects.Add(consult.Object);
                    }
                    else
                    {
                        //No esta Seleccionado
                    }

                }

                Session["Carrito"] = result.Objects;
                return View("AddGetAll", result);
            }

            else
            {
                var result = new ML.Result();
                result.Objects = new List<Object>();
                result.Objects = (List<Object>)Session["Carrito"];
                int pos = 0;


                foreach (ML.Producto productoItem in producto.ToList())
                {

                    if (productoItem.Selected == true)
                    {

                        if (productoItem.IdProducto == producto[pos].IdProducto)
                        {
                            producto[pos].DetalleVenta = new ML.DetalleVenta();
                            producto[pos].DetalleVenta.Cantidad++;
                            producto[pos].DetalleVenta.Venta.Total =Convert.ToInt32(producto[pos].DetalleVenta.Cantidad * producto[pos].Precio);
                        }
                        else
                        {
                            productos.IdProducto = productoItem.IdProducto;
                            var consult = BL.Producto.GetByIdEF(productos);
                            result.Objects.Add(consult.Object);
                            Session["Carrito"] = result.Objects;
                        }


                    }
                    else
                    {
                        //No esta Seleccionado
                    }
                    pos++;
                }
                return View("GetAll", result);

            }
        }
   
    */
    
        /*public ActionResult GetAll()
         {
             return View(BL.Pedido.GetEFSP());

        }*/
        public ActionResult GetVenta(int IdPedido)
        {
            ML.DetalleVenta pedido = new ML.DetalleVenta();
            pedido.Venta = new ML.Venta();
            pedido.Venta.IdVenta = IdPedido;

            return View(BL.DetalleVenta.GetByIdEF(pedido));
        }
    }
}