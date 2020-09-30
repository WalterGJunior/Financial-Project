using DAL;
using DAL.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class ConsultarMovController : Controller
    {
        //
        // GET: /ConsultarMov/

        public ActionResult ConsultarMovimento()
        {
            CarregarCategoria();
            return View();
        }

        private void CarregarCategoria()
        {
            CategoriaDAO objDAO = new CategoriaDAO();

            ViewBag.ListaCategoria = objDAO.ConsultarCategoria(1);
        }

        public ActionResult RealizarConsulta(string tipo_movimento, string cod_categoria, string data_entrada, string data_saida)
        {
            ViewBag.TipoMovimento = tipo_movimento;
            ViewBag.CodigoCategoria = cod_categoria;
            ViewBag.DataEntrada = data_entrada;
            ViewBag.DataSaida = data_saida;

            if (data_entrada == null || data_entrada.Trim() == string.Empty ||
                data_saida == null || data_saida.Trim() == string.Empty)
            {
                ViewBag.Validar = 0;
            }
            else
            {
                MovimentoDAO objDAO = new MovimentoDAO();

                ViewBag.ListaMovimento = objDAO.RealizarConsulta(1, tipo_movimento, cod_categoria, data_entrada, data_saida);
            }

            CarregarCategoria();
            return View("ConsultarMovimento");
        }

    }
}
