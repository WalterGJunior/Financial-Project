using DAL;
using DAL.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class EmpresaController : PageBase
    {
        //
        // GET: /Empresa/

        public ActionResult Empresa()
        {
            ConsultarEmpresa();
            return View();
        }
        public ActionResult GravarEmpresa(string nome_empresa, string endereco, string telefone, int? cod_empresa )
        {
            if (nome_empresa.Trim() == string.Empty || endereco.Trim() == 
                                       string.Empty || telefone.Trim() == 
                                       string.Empty)
            {
                ViewBag.Validar = 0;
            }
            else
            {

                try
                {
                    //Cria o Objeto DAO
                    EmpresaDAO objDAO = new EmpresaDAO();

                    //Cria o objeto para gravar
                    tb_empresa objEmpresa = new tb_empresa();
                    tb_endereco objEndereco = new tb_endereco();

                    objEmpresa.cod_usuario = CodigoUsuarioLogado;
                    objEmpresa.nome_empresa = nome_empresa.Trim();
                    objEndereco.endereco = endereco.Trim();
                    objEndereco.telefone = telefone.Trim();

                    objEmpresa.tb_endereco.Add(objEndereco);

                    //Verifica se é uma inserção
                    if (cod_empresa == null)
                    {
                        objDAO.InserirEmpresa(objEmpresa);
                    }
                    else
                    {
                        objEmpresa.cod_empresa = Convert.ToInt32(cod_empresa);
                        objDAO.AlterarEmpresa(objEmpresa);
                    }

                    ViewBag.Validar = 1;

                }
                catch
                {
                    ViewBag.Validar = -1;
                }
            }

            ConsultarEmpresa();

            return View("Empresa");
        }
        public ActionResult AlterarEmpresa(string nome_empresa, int cod_empresa, string endereco, string telefone)
        {
            ViewBag.NomeEmpresa = nome_empresa;
            ViewBag.CodigoEmpresa = cod_empresa;
            ViewBag.Endereco = endereco;
            ViewBag.Telefone = telefone;

            ConsultarEmpresa();
            return View("Empresa");
        }


        private void ConsultarEmpresa()
        {
            EmpresaDAO objDAO = new EmpresaDAO();
            List<EmpresaVO> lstCat = objDAO.ConsultarEmpresa(CodigoUsuarioLogado);

            ViewBag.ListaEmpresa = lstCat;
        }
    }
}
