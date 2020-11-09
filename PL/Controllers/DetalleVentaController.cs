using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using System.Text;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Drawing;
using Syncfusion.Pdf.Grid;


namespace PL.Controllers
{
    public class DetalleVentaController : Controller
    {
        // GET: DetalleVenta
        public ActionResult GetAll()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<Object>();
            result.Objects = (List<Object>)Session["Carrito"];

            ML.Venta pedido = new ML.Venta();
            pedido.Cliente = new ML.Cliente();
            pedido.Cliente.IdCliente = 1;

            double total = 0;

            if (result.Objects == null)
            {
                return View("GetAll", result);
            }
            else
            {
                foreach (ML.Producto producto in result.Objects.ToList())
                {
                    total = total + producto.DetalleVenta.Venta.Total;
                    int IdVenta = producto.DetalleVenta.Venta.IdVenta;
                }

                pedido.Total = (Int32)total;

                var AddVenta = BL.Venta.AddEF(pedido, result.Objects);
            }
            return View("GetAll", result);

        }

        public ActionResult VerCompras()
        {
            ML.DetalleVenta detalleVenta = new ML.DetalleVenta();
            ML.Result result = BL.DetalleVenta.GetAll();
            detalleVenta.DetalleVentas = result.Objects;
            return View(detalleVenta);
        }

        //[HttpPost]
        //[ValidateInput(false)]
        //public FileResult Export(string GridHtml)
        //{
        //    using (MemoryStream stream = new System.IO.MemoryStream())
        //    {
        //        StringReader sr = new StringReader(GridHtml);
        //        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        //        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
        //        pdfDoc.Open();
        //        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
        //        pdfDoc.Close();
        //        return File(stream.ToArray(), "application/pdf", "Grid.pdf");
        //    }
        //}

        public FileResult CreateDocument()
        {
            using (PdfDocument document = new PdfDocument())
            {
                //Create a new PDF document.
                PdfDocument doc = new PdfDocument();
                //Adds page settings
                document.PageSettings.Orientation = PdfPageOrientation.Landscape;
                document.PageSettings.Margins.All = 50;

                //Add a page.
                PdfPage page = doc.Pages.Add();
                PdfGraphics graphics = page.Graphics;
                //Create a PdfGrid.
                PdfGrid pdfGrid = new PdfGrid();
                PdfImage image = PdfImage.FromFile(Server.MapPath("../Content/img/índice.png"));
                RectangleF bounds = new RectangleF(0, 0, 150, 150);
                //Draws the image to the PDF page
                page.Graphics.DrawImage(image, bounds);
                //Create a DataTable.
                DataTable dataTable = new DataTable();
                
                //Add columns to the DataTable
                dataTable.Columns.Add("IdVenta");
                dataTable.Columns.Add("Nombre Producto");
                dataTable.Columns.Add("Descripcion");
                dataTable.Columns.Add("Precio");
                dataTable.Columns.Add("Fecha");
                dataTable.Columns.Add("Cantidad");
                dataTable.Columns.Add("Total");
                //Add rows to the DataTable.
                ML.DetalleVenta detalleVenta = new ML.DetalleVenta();
                detalleVenta.Venta = new ML.Venta();
                detalleVenta.Producto = new ML.Producto();
                ML.Result result = BL.DetalleVenta.GetAll();
                detalleVenta.DetalleVentas = result.Objects;

                foreach (ML.DetalleVenta detalle in detalleVenta.DetalleVentas)
                {
                    dataTable.Rows.Add(new object[] { detalle.Venta.IdVenta,detalle.Producto.Nombre, detalle.Producto.Descripcion,
                    detalle.Producto.Precio, detalle.Venta.Fecha, detalle.Cantidad, detalle.Venta.Total});
                }



                //Assign data source.
                pdfGrid.DataSource = dataTable;
                //Draw grid to the page of PDF document.
                pdfGrid.Draw(page, new PointF(10, 10));
                // Open the document in browser after saving it
                doc.Save("Output.pdf", HttpContext.ApplicationInstance.Response, HttpReadType.Save);
                //close the document
                doc.Close(true);
            }
            return null;
        }
    }
}