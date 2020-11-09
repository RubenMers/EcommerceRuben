using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string PaisOrigen { get; set; }

        public decimal Precio { get; set; }

        public ML.SubCategoria SubCategoria { get; set; }

        public List<object> Productos { get; set; }

        public byte[] LogoTipo { get; set; }

        public ML.DetalleVenta DetalleVenta { get; set; }

        public ML.Venta Venta { get; set; }

        public int Stock { get; set; }

        public bool Status { get; set; }
    }
}
