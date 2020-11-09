using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DetalleVenta
    {
        public static ML.Result GetByIdEF(ML.DetalleVenta detalle)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.EcommerceRubenEntities context = new DL_EF.EcommerceRubenEntities())
                {


                    var alumnos = context.DetalleVentaGetAllInner().FirstOrDefault();




                    if (alumnos != null)
                    {

                        ML.DetalleVenta detalles = new ML.DetalleVenta();
                        ML.Producto producto = new ML.Producto();
                        detalles.Producto.IdProducto = alumnos.IdProducto;
                        detalles.Producto.Nombre = alumnos.Nombre;
                        detalles.Producto.Precio = alumnos.Precio;


                        detalles.Producto.LogoTipo = Convert.FromBase64String(alumnos.Imagen);


                        result.Object = producto;

                        /*foreach (var obj in alumnos)
                        {
                         
                        }*/

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

        public static ML.Result Add(int IdVenta, int IdProducto, int Cantidad)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.EcommerceRubenEntities context = new DL_EF.EcommerceRubenEntities())
                {
                    var query = context.DetalleVentaAdd(IdVenta, IdProducto, Cantidad);
                    var query2 = context.ProductoStock(IdProducto, Cantidad);

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
                //using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                //{
                //    string Query = "Insert into DetallePedido values (@IdProducto,@IdPedido,@cantidad)";
                //    SqlCommand cmd = DL.Conexion.CreateCommand(Query, context);

                //    cmd.Parameters.AddWithValue("@IdPedido", IdVenta);
                //    cmd.Parameters.AddWithValue("@IdProducto", IdProducto);
                //    cmd.Parameters.AddWithValue("@Cantidad", Cantidad);

                //    int RowsAffected = DL.Conexion.ExecuteCommand(cmd);
                //    if (RowsAffected >= 1)
                //    {
                //        result.Correct = true;
                //    }
                //    else
                //    {
                //        result.Correct = false;
                //        result.ErrorMessage = "No se inserto correctamente";
                //    }
                //}
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.EcommerceRubenEntities contex = new DL_EF.EcommerceRubenEntities())
                {
                    var query = contex.VentasGetAll().ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.DetalleVenta detalle = new ML.DetalleVenta();
                            detalle.Venta = new ML.Venta();
                            detalle.Producto = new ML.Producto();
                            detalle.Venta.Cliente=new ML.Cliente();
                            detalle.Venta.Cliente.Nombre=obj.NombreCliente;
                            detalle.Producto.IdProducto = obj.IdProducto;
                            detalle.Producto.Nombre = obj.NombreProducto;
                            detalle.Producto.Descripcion = obj.Descripcion;
                            detalle.Producto.Precio = obj.Precio;
                            detalle.Venta.IdVenta = obj.IdVenta;
                            detalle.Venta.Fecha = Convert.ToDateTime(obj.Fecha);
                            detalle.Venta.Total = Convert.ToInt32(obj.Total);
                            detalle.Cantidad = Convert.ToInt32(obj.Cantidad);
                            result.Objects.Add(detalle);
                        }
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
    }
}
