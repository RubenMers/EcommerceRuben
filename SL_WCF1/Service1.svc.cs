using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SL_WCF1
{

    public class Service1 : IService1
    {
        public Result AddProducto(ML.Producto producto)
        {

            ML.Result result = BL.Producto.AddImagen(producto);

            return new Result { Correct = result.Correct, ErrorMessage = result.ErrorMessage, Ex = result.Ex };
        }

        public Result UpdateProducto(ML.Producto producto)
        {

            ML.Result result = BL.Producto.UpdateEF(producto);

            return new Result { Correct = result.Correct, ErrorMessage = result.ErrorMessage, Ex = result.Ex };
        }

        public Result DeleteProducto(ML.Producto producto)
        {

            ML.Result result = BL.Producto.DeleteEF(producto);

            return new Result { Correct = result.Correct, ErrorMessage = result.ErrorMessage, Ex = result.Ex };
        }
    }
}
