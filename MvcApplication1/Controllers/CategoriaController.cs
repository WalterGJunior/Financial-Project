using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class CategoriaController : PageBase
    {
        //
        // GET: /Categoria/

        public ActionResult Categoria()
        {
            ConsultarCategoria();
            return View();
        }

        public ActionResult GravarCategoria(string nome_categoria, int? cod_categoria)
        {
            if (nome_categoria.Trim() == string.Empty)
            {
                ViewBag.Validar = 0;
            }
            else
            {

                try
                {
                    //Cria o Objeto DAO
                    CategoriaDAO objDAO = new CategoriaDAO();

                    //Cria o objeto para gravar
                    tb_categoria objCategoria = new tb_categoria();

                    objCategoria.cod_usuario = CodigoUsuarioLogado;
                    objCategoria.nome_categoria = nome_categoria.Trim();

                    //Verifica se é uma inserção
                    if (cod_categoria == null)
                    {
                        objDAO.InserirCategoria(objCategoria);
                    }
                    else
                    {
                        objCategoria.cod_categoria = Convert.ToInt32(cod_categoria);
                        objDAO.AlterarCategoria(objCategoria);
                    }
                    

                    ViewBag.Validar = 1; 

                }
                catch 
                {
                    ViewBag.Validar = -1;                     
                }
            }

            ConsultarCategoria();
            return View("Categoria");
        }

        public ActionResult AlterarCategoria(string nome_categoria, int cod_categoria)
        {
            ViewBag.NomeCategoria = nome_categoria;
            ViewBag.Codigocategoria = cod_categoria;

            ConsultarCategoria();
            return View("Categoria");
        }


        private void ConsultarCategoria()
        {
            CategoriaDAO objDAO = new CategoriaDAO();
            List<tb_categoria> lstCat = objDAO.ConsultarCategoria(CodigoUsuarioLogado);
            ViewBag.ListaCategoria = lstCat;
        }

    }
}
