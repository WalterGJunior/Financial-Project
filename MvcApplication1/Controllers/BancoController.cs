using DAL;
using DAL.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class BancoController : PageBase
    {
        //
        // GET: /Banco/

        public ActionResult Banco()
        {
            ConsultarBanco();
            return View();
        }
        public ActionResult GravarBanco(string nome_banco, string endereco, string telefone, string site_banco, int? cod_banco)
        {
            if (nome_banco.Trim() == string.Empty || endereco.Trim() == 
                                     string.Empty || telefone.Trim() ==
                                     string.Empty || site_banco.Trim() == string.Empty)
            {
                ViewBag.Validar = 0;
            }
            else
            {

                try
                {
                    //Cria o Objeto DAO
                    BancoDAO objDAO = new BancoDAO();

                    //Cria o objeto para gravar
                    tb_banco objBanco = new tb_banco();
                    tb_endereco objEndereco = new tb_endereco();

                    objBanco.cod_usuario = CodigoUsuarioLogado;
                    objBanco.nome_banco = nome_banco.Trim();
                    objEndereco.endereco = endereco.Trim();
                    objEndereco.telefone = telefone.Trim();
                    objBanco.site_banco = site_banco.Trim();

                    objBanco.tb_endereco.Add(objEndereco);

                    if (cod_banco == null)
                    {
                        objDAO.InserirBanco(objBanco);
                    }
                    else
                    {
                        objBanco.cod_banco = Convert.ToInt32(cod_banco);
                        objDAO.AlterarBanco(objBanco);
                    }
                    
                    ViewBag.Validar = 1;

                }
                catch
                {
                    ViewBag.Validar = -1;
                }
            }

            ConsultarBanco();

            return View("Banco");
        }
        public ActionResult AlterarBanco(string nome_banco, int cod_banco, string endereco, string telefone, string site_banco)
        {
            ViewBag.NomeBanco = nome_banco;
            ViewBag.CodigoBanco = cod_banco;
            ViewBag.Endereco = endereco;
            ViewBag.Telefone = telefone;
            ViewBag.SiteBanco = site_banco;

            ConsultarBanco();
            return View("Banco");
        }

        public void ConsultarBanco()
        {
            BancoDAO objDAO = new BancoDAO();
            List<BancoVO> lstCat = objDAO.ConsultarBanco(CodigoUsuarioLogado);

            ViewBag.ListaBanco = lstCat;
        }


    }
}
