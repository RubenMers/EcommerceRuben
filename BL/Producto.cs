using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BL
{
    public class Producto
    {

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.EcommerceRubenEntities contex = new DL_EF.EcommerceRubenEntities())
                {
                    var query = contex.ProductoGetAll().ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        result.Correct = true;
                        foreach (var obj in query)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.IdProducto = obj.IdProducto;
                            producto.Nombre = obj.Nombre;
                            producto.Descripcion = obj.Descripcion;
                            producto.PaisOrigen = obj.PaisOrigen;
                            producto.Precio = obj.Precio;
                            producto.SubCategoria = new ML.SubCategoria();
                            producto.SubCategoria.IdSubcategoria = obj.IdSubCategoria;
                            producto.LogoTipo = Convert.FromBase64String(obj.Imagen);
                            producto.Stock = Convert.ToInt32(obj.Stock);
                            producto.Status = Convert.ToBoolean(obj.Status);
                            result.Objects.Add(producto);
                        }
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result AddImagen(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.EcommerceRubenEntities contex = new DL_EF.EcommerceRubenEntities())
                {
                    var Add = contex.ProductoAddImagen
                        (producto.Nombre, producto.Descripcion, producto.PaisOrigen, producto.Precio
                        ,producto.SubCategoria.IdSubcategoria,
                        Convert.ToBase64String(producto.LogoTipo), producto.Stock);

                    if (Add > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetIdProducto(int IdProducto)
        {

            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.EcommerceRubenEntities context = new DL_EF.EcommerceRubenEntities())
                {

                    var obj = context.ProductoGetById(IdProducto).FirstOrDefault();

                    //ML.Producto producto = new ML.Producto();

                    if (obj != null)
                    {
                        ML.DetalleVenta detalle = new ML.DetalleVenta();
                        ML.Producto producto = new ML.Producto();
                        producto.IdProducto = obj.IdProducto;
                        producto.Nombre = obj.NombreProducto;
                        producto.Descripcion = obj.Descripcion;
                        producto.PaisOrigen = obj.PaisOrigen;
                        producto.Precio = obj.Precio;
                        producto.SubCategoria = new ML.SubCategoria();
                        producto.SubCategoria.IdSubcategoria = obj.IdSubCategori;
                        producto.LogoTipo = Convert.FromBase64String(obj.Imagen);
                        producto.Stock = Convert.ToInt32(obj.Stock);
                        producto.DetalleVenta = new ML.DetalleVenta();
                        producto.DetalleVenta.Cantidad = 1;

                        producto.DetalleVenta.Venta = new ML.Venta();
                        producto.DetalleVenta.Venta.Total = Convert.ToInt32(producto.Precio * producto.DetalleVenta.Cantidad);
                        //producto.DetalleVenta.Venta.Total = alumnos.Precio;
                        result.Object = producto;
                    }

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result UpdateEF(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try
            {

                using (DL_EF.EcommerceRubenEntities context = new DL_EF.EcommerceRubenEntities())
                {
                    var updateResult = context.ProductoUpdate(producto.IdProducto, producto.Nombre, producto.Descripcion, producto.PaisOrigen,
                        producto.Precio, producto.SubCategoria.IdSubcategoria, Convert.ToBase64String(producto.LogoTipo),producto.Stock);

                    if (updateResult >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizó el status de la credencial";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    
        public static ML.Result DeleteEF(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.EcommerceRubenEntities context = new DL_EF.EcommerceRubenEntities())
                {

                    var query = context.ProductoDelete(producto.IdProducto);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se eliminó el registro";
                    }

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static ML.Result Get()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.EcommerceRubenEntities context = new DL_EF.EcommerceRubenEntities())
                {

                    var query = context.ProductoGetAllInnerJoin2().ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.IdProducto = obj.IdProducto;
                            producto.Nombre = obj.Nombre;
                            producto.Descripcion = obj.Descripcion;
                            producto.PaisOrigen = obj.PaisOrigen;
                            producto.Precio = obj.Precio;
                            producto.SubCategoria = new ML.SubCategoria();
                            producto.SubCategoria.Nombre = obj.NombreCategoria;
                            producto.LogoTipo = Convert.FromBase64String(obj.Imagen);
                            producto.Stock = Convert.ToInt32(obj.Stock);



                            result.Objects.Add(producto);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }

        public static ML.Result GetByIdEF(ML.Producto productos)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.EcommerceRubenEntities context = new DL_EF.EcommerceRubenEntities())
                {


                    var alumnos = context.ProductoGetById(productos.IdProducto).FirstOrDefault();




                    if (alumnos != null)
                    {

                        ML.DetalleVenta detalle = new ML.DetalleVenta();
                        ML.Producto producto = new ML.Producto();
                        producto.IdProducto = alumnos.IdProducto;
                        producto.Nombre = alumnos.NombreProducto;
                        producto.Descripcion = alumnos.Descripcion;
                        producto.PaisOrigen = alumnos.PaisOrigen;
                        producto.Precio = alumnos.Precio;
                        producto.Stock = Convert.ToInt32(alumnos.Stock);

                        producto.SubCategoria = new ML.SubCategoria();
                        producto.SubCategoria.IdSubcategoria = alumnos.IdSubCategori;
                        producto.SubCategoria.Categoria = new ML.Categoria();
                        producto.SubCategoria.Categoria.IdCategoria = alumnos.IdCategoria;
                        producto.LogoTipo = Convert.FromBase64String(alumnos.Imagen);

                        producto.DetalleVenta = new ML.DetalleVenta();
                        producto.DetalleVenta.Cantidad = 1;

                        producto.DetalleVenta.Venta = new ML.Venta();
                        producto.DetalleVenta.Venta.Total = Convert.ToInt32(producto.Precio * producto.DetalleVenta.Cantidad);
                        //producto.DetalleVenta.Venta.Total = alumnos.Precio;
                        result.Object = producto;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }

        public static ML.Result Baja(int IdProducto, bool Status)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (DL_EF.EcommerceRubenEntities context = new DL_EF.EcommerceRubenEntities())
                {
                    var updateResult = context.ProductoBaja(IdProducto, Status);

                    if (updateResult >= 1)
                    {
                        result.Correct = true;
                        result.ErrorMessage = "Se actualizo correctamente el status del producto";
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizaron los datos del producto";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static ML.Result Alta(int IdProducto, bool Status)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (DL_EF.EcommerceRubenEntities context = new DL_EF.EcommerceRubenEntities())
                {
                    var updateResult = context.ProductoAlta(IdProducto, Status);

                    if (updateResult >= 1)
                    {
                        result.Correct = true;
                        result.ErrorMessage = "Se actualizo correctamente el status del producto";
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizaron los datos del producto";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
