using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Venta
    {
        public static ML.Result AddEF(ML.Venta venta, List<Object> Objects)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.EcommerceRubenEntities context = new DL_EF.EcommerceRubenEntities())
                {
                    //var query = context.VentaAddSP(venta.Total, venta.Cliente.IdCliente);


                    var IdResult = new ObjectParameter("IdVenta", typeof(int));
                    int ventas = context.VentaAddSP(IdResult, venta.Total, venta.Cliente.IdCliente);
                    venta.IdVenta = (int)IdResult.Value;

                    //int IdUsuario = Convert.ToInt32(venta.Cliente.IdCliente);
                    //double Total = Convert.ToDouble(venta.Total);
                    ////int Ventas = 1;

                    //var IdResult = new ObjectParameter("IdVenta", typeof(int));
                    //int IdVenta = Convert.ToInt32(context.VentaAddSP(IdResult, venta.Total, venta.Cliente.IdCliente));

                    Console.WriteLine("El IdVenta es: " + venta.IdVenta);



                    /* foreach (ML.DetalleVenta detalleVenta in Objects)
                     {
                         detalleVenta.Venta = venta;

                         BL.DetalleVenta.AddEF(detalleVenta);

                     }*/

                    foreach (ML.Producto productoItem in Objects)
                    {

                        BL.DetalleVenta.Add(venta.IdVenta, productoItem.IdProducto, productoItem.DetalleVenta.Cantidad);
                    }

                    result.Object = venta.IdVenta;

                    if (venta.IdVenta >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se insertó el registro";
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
    }
}
