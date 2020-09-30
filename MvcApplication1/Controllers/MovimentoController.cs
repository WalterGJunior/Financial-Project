using DAL;
using DAL.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.VO;

namespace MvcApplication1.Controllers
{
    public class MovimentoController : PageBase
    {
        //
        // GET: /Movimento/

        public ActionResult Movimento()
        {
            
            CarregarEmpresa();
            CarregarConta();
            CarregarCategoria();
            return View();
        }

        private void CarregarEmpresa()
        {
            EmpresaDAO objDAO = new EmpresaDAO();

            ViewBag.ListaEmpresa = objDAO.ConsultarEmpresa(CodigoUsuarioLogado);
        }

        private void CarregarConta()
        {
            ContaDAO objDAO = new ContaDAO();

            ViewBag.ListaConta = objDAO.ConsultarContaAtiva(CodigoUsuarioLogado);
        }

        private void CarregarCategoria()
        {
            CategoriaDAO objDAO = new CategoriaDAO();

            ViewBag.ListaCategoria = objDAO.ConsultarCategoria(CodigoUsuarioLogado);
        }


        public ActionResult AlterarMovimento(string TipoMovimento, DateTime? DataMovimento, decimal? ValorMovimento,
                                            int? CodigoConta, int? CodigoCategoria, int? CodigoEmpresa, string ObsMovimento, int? NumeroConta)
        {
            ViewBag.TipoMovimento = TipoMovimento;
            ViewBag.DataMovimento = DataMovimento;
            ViewBag.ValorMovimento = ValorMovimento;
            ViewBag.CodConta = CodigoConta;
            ViewBag.CodCategoria = CodigoCategoria;
            ViewBag.CodigoEmpresa = CodigoEmpresa;
            ViewBag.ObsMovimento = ObsMovimento;
            ViewBag.NumeroConta = NumeroConta;

            
            CarregarEmpresa();
            CarregarConta();
            CarregarCategoria();
            return View("Movimento");
        }

        public ActionResult GravarMovimento(string tipo_movimento, DateTime? data_movimento, decimal? valor_movimento, 
                                            int? cod_conta, int? cod_categoria, int? cod_empresa, string obs_movimento)
        {
            ViewBag.TipoMovimento = tipo_movimento;
            ViewBag.DataMovimento = data_movimento;
            ViewBag.ValorMovimento = valor_movimento;
            ViewBag.CodigoConta = cod_conta;
            ViewBag.CodigoCategoria = cod_categoria;
            ViewBag.CodigoEmpresa = cod_empresa;
            ViewBag.ObsMovimento = obs_movimento;

            if (tipo_movimento == string.Empty || data_movimento == null || valor_movimento == null || cod_conta == null ||
                cod_categoria == null || cod_empresa == null)
            {
                ViewBag.Validar = 0;
            }
            else
            {
                try
                {
                    MovimentoDAO objDAO = new MovimentoDAO();

                    tb_movimento objMovimento = new tb_movimento();

                    objMovimento.cod_usuario = CodigoUsuarioLogado;
                    objMovimento.tipo_movimento = Convert.ToInt16(tipo_movimento);
                    objMovimento.data_movimento = Convert.ToDateTime(data_movimento);
                    objMovimento.valor_movimento = Convert.ToDecimal(valor_movimento);
                    objMovimento.cod_conta = Convert.ToInt32(cod_conta);
                    objMovimento.cod_categoria = Convert.ToInt32(cod_categoria);
                    objMovimento.cod_empresa = Convert.ToInt32(cod_empresa);
                    objMovimento.obs_movimento = obs_movimento;

                    if (tipo_movimento == "1")
                    {
                        objDAO.RealizarEntrada(objMovimento);
                    }
                    else
                    {
                        objDAO.RealizarSaida(objMovimento);
                    }

                    ViewBag.Validar = 1;

                    ViewBag.TipoMovimento = null;
                    ViewBag.DataMovimento = null;
                    ViewBag.ValorMovimento = null;
                    ViewBag.CodigoConta = null;
                    ViewBag.CodigoCategoria = null;
                    ViewBag.CodigoEmpresa = null;
                    ViewBag.ObsMovimento = null;
                }
                catch
                {
                    ViewBag.Validar = -1;                    
                }
            }
            
            CarregarEmpresa();
            CarregarConta();
            CarregarCategoria();
            return View("Movimento");            
        }


    }
}
