using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Producto.GetAll();

            ML.Producto producto = new ML.Producto();

            producto.Productos = result.Objects;

            return View(producto);
        }
        [HttpGet]
        public ActionResult Form(int? IdProducto)
        {
            ML.Producto producto = new ML.Producto();



            if (IdProducto == null)
            {
                ViewBag.Titulo = "Registrar Producto";
                ViewBag.Accion = "Guardar";

                producto.SubCategoria = new ML.SubCategoria();

                ML.Result resultCategorias = BL.Categoria.GetAllEF();
                producto.SubCategoria.Categoria = new ML.Categoria();
                producto.SubCategoria.Categoria.Categorias = resultCategorias.Objects;



                return View(producto);
            }
            else
            {
                ViewBag.Titulo = "Actualizar Producto";
                ViewBag.Accion = "Actualizar";




                producto.IdProducto = IdProducto.Value;

                var result = BL.Producto.GetByIdEF(producto);


                if (result.Object != null)
                {
                    producto.SubCategoria = new ML.SubCategoria();
                    producto.Nombre = ((ML.Producto)result.Object).Nombre;
                    producto.Descripcion = ((ML.Producto)result.Object).Descripcion;
                    producto.PaisOrigen = ((ML.Producto)result.Object).PaisOrigen;
                    producto.Precio = ((ML.Producto)result.Object).Precio;
                    producto.SubCategoria = new ML.SubCategoria();
                    producto.SubCategoria.IdSubcategoria = ((ML.Producto)result.Object).SubCategoria.IdSubcategoria;
                    producto.SubCategoria.Categoria = new ML.Categoria();
                    producto.SubCategoria.Categoria.IdCategoria = ((ML.Producto)result.Object).SubCategoria.Categoria.IdCategoria;
                    producto.LogoTipo = ((ML.Producto)result.Object).LogoTipo;
                    producto.Stock = ((ML.Producto)result.Object).Stock;


                    ML.Result resultCategorias = BL.Categoria.GetAllEF();
                    producto.SubCategoria.Categoria = new ML.Categoria();
                    producto.SubCategoria.Categoria.Categorias = resultCategorias.Objects;



                    return View(producto);
                }
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return PartialView("Validation");
                }
            }

        }

        public JsonResult GetSubCategoria(int IdCategoria)
        {
            {
                ML.SubCategoria subCategoria = new ML.SubCategoria();
                subCategoria.Categoria = new ML.Categoria();
                subCategoria.Categoria.IdCategoria = IdCategoria;

                var result = BL.SubCategoria.GetByIdCategoria(subCategoria);

                // ML.Result resultProductoss = BL.SubCategoria.GetAllEF();


                List<SelectListItem> opciones = new List<SelectListItem>();

                opciones.Add(new SelectListItem { Text = "--Selecciona--", Value = "0" });

                if (result.Objects != null)
                {
                    /*ML.Producto producto = new ML.Producto();
                    producto.SubCategoria = new ML.SubCategoria();
                    producto.SubCategoria.SubCategorias = resultProductoss.Objects;*/

                    foreach (ML.SubCategoria SubCategorias in result.Objects)
                    {


                        opciones.Add(new SelectListItem { Text = SubCategorias.Nombre.ToString(), Value = SubCategorias.IdSubcategoria.ToString() });
                    }
                }

                return Json(new SelectList(opciones, "Value", "Text", JsonRequestBehavior.AllowGet));
            }




        }

        [HttpPost]
        public ActionResult Form(ML.Producto producto, HttpPostedFileBase Imagen)
        {
            ML.Result result = new ML.Result();

            if (producto.IdProducto == 0)
            {

                //result = BL.Producto.AddEF(producto);
                producto.LogoTipo = ConvertToBytes(Imagen);
                result = BL.Producto.AddImagen(producto);

                if (result.Correct == true)
                {
                    ViewBag.Message = "Se agrego el producto correctamente";
                    return PartialView("Validation");
                }
                else
                {
                    ViewBag.Message = "Ocurrió un error al agregar el producto.  Error: " + result.ErrorMessage;
                    return PartialView("Validation");
                }
            }
            else
            {

                producto.LogoTipo = ConvertToBytes(Imagen);
                result = BL.Producto.UpdateEF(producto);

                if (result.Correct == true)
                {
                    ViewBag.Message = "Se actualizo correctamente el producto ";
                    return PartialView("Validation");
                }
                else
                {
                    ViewBag.Message = "Ocurrió un error al actualizar el producto.  Error: " + result.ErrorMessage;
                    return PartialView("Validation");
                }
            }

        }

        public ActionResult Delete(ML.Producto producto)
        {
            //ML.Result result = BL.SubCategoria(subCategoria.IdSubcategoria);

            ML.Result result = BL.Producto.DeleteEF(producto);

            if (result.Correct == true)
            {
                ViewBag.Mesaage = "El producto se ha eliminado correctamente";
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al eliminar el producto. Error: " + result.ErrorMessage;
            }
            return PartialView("Validation");
        }

        public byte[] ConvertToBytes(HttpPostedFileBase Imagen)
        {

            byte[] data = null;
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Imagen.InputStream);
            data = reader.ReadBytes((int)Imagen.ContentLength);

            return data;
        }

        public ActionResult UpdateStatus(int IdProducto, bool Status)
        {

            if (Status == false)
            {
                var result = BL.Producto.Baja(IdProducto, Status);
                ViewBag.Message = "Se ha cambiado el status correctamente";
            }
            else
            {
                var result = BL.Producto.Alta(IdProducto, Status);
                ViewBag.Message = "Se ha cambiado el status correctamente";
            }

            return PartialView("Validation");
        }
    }
}