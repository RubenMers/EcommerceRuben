using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SubCategoria
    {
        public static ML.Result GetByIdCategoria(ML.SubCategoria subCategoria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.EcommerceRubenEntities context = new DL_EF.EcommerceRubenEntities())
                {

                    //var IdResult = new ObjectParameter("IdSubCategoria", typeof(int));
                    var Query = context.SubCategoriaGetByIdCategoria(subCategoria.Categoria.IdCategoria);



                    result.Objects = new List<object>();

                    if (Query != null)
                    {
                        foreach (var obj in Query)
                        {
                            ML.SubCategoria alumno = new ML.SubCategoria();
                            alumno.IdSubcategoria = obj.IdSubCategoria;
                            alumno.Nombre = obj.Nombre;
                            alumno.Categoria = new ML.Categoria();
                            alumno.Categoria.IdCategoria = obj.IdCategoria;

                            result.Objects.Add(alumno);
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

        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.EcommerceRubenEntities context = new DL_EF.EcommerceRubenEntities())
                {

                    var query = context.SubCategoriaGetAll().ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.SubCategoria alumno = new ML.SubCategoria();
                            alumno.IdSubcategoria = obj.IdSubCategoria;
                            alumno.Nombre = obj.Nombre;
                            alumno.Categoria = new ML.Categoria();
                            alumno.Categoria.IdCategoria = obj.IdCategoria;

                            result.Objects.Add(alumno);
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
    }
}
