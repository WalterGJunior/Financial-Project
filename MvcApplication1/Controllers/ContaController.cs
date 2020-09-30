using System;
using DAL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.DAO;
using DAL.VO;

namespace MvcApplication1.Controllers
{
    public class ContaController : PageBase
    {
        //
        // GET: /Conta/

        public ActionResult Conta()
        {
            CarregarBanco();
            CarregarConta();
            return View();
        }

        private void CarregarBanco()
        {
            BancoDAO objDAO = new BancoDAO();

            ViewBag.ListaBanco = objDAO.ConsultarBanco(CodigoUsuarioLogado);
        }
        
        private void CarregarConta()
        { 
            ContaDAO objDAO = new ContaDAO();

            List<ContaVO> lstConta = objDAO.ConsultarConta(CodigoUsuarioLogado);

            ViewBag.ListaConta = lstConta;
        }

        public ActionResult AlterarConta(string NumeroConta, string CodigoConta, string SaldoConta, string StatusConta, string CodigoBanco)
        {
            ViewBag.CodigoConta = CodigoConta;
            ViewBag.NumeroConta = NumeroConta;
            ViewBag.SaldoConta = SaldoConta;
            ViewBag.StatusConta = StatusConta;
            ViewBag.CodigoBanco = CodigoBanco;

            CarregarBanco();
            CarregarConta();
            return View("Conta");
        }

        public ActionResult GravarConta(int? cod_conta, string numero_conta, string saldo_conta, int cod_banco, string status_conta)
        {
            if (numero_conta.Trim() == string.Empty || saldo_conta.Trim() == string.Empty || cod_banco == null)
            {
                ViewBag.Validar = 0;
            }
            else
            {              
                try
                {
                    ContaDAO objDAO = new ContaDAO();
                    tb_conta objConta = new tb_conta();

                    objConta.cod_usuario = CodigoUsuarioLogado;
                    objConta.numero_conta = numero_conta;
                    objConta.saldo_conta = Convert.ToDecimal(saldo_conta);
                    objConta.cod_banco = Convert.ToInt32(cod_banco);
                    objConta.status_conta = Convert.ToBoolean(status_conta);

                    if (cod_conta == null)
                    {
                        objDAO.InserirConta(objConta);
                    }
                    else
                    {
                        objConta.cod_conta = Convert.ToInt32(cod_conta);
                        objDAO.AlterarConta(objConta);
                    }

                    ViewBag.Validar = 1;
                }
                catch 
                {

                    ViewBag.Validar = -1; 
                }

            }
            CarregarConta();
            CarregarBanco();
            return View("Conta");
        }

    }
}
