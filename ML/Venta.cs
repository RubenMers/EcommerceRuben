using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Venta
    {
        public int IdVenta { get; set; }

        public DateTime Fecha { get; set; }

        public int Total { get; set; }

        public int IdCliente { get; set; }

        public ML.Cliente Cliente { get; set; }
    }
}
