using DAL;
using DAL.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class LoginController : PageBase
    {
        //
        // GET: /Login/

        public ActionResult Login()
        {
            return View();
        }

        public void Deslogar()
        {
            CodigoUsuarioLogado = 0;
            NomeUsuarioLogado = string.Empty;

            Response.Redirect("/Login/Login");
        }

        public ActionResult ValidarLogin(string email_usuario, string senha_usuario)
        {
            if (email_usuario.Trim() == string.Empty || senha_usuario.Trim() == string.Empty)
            {
                ViewBag.Validar = 0;
            }
            else
            {
                UsuarioDAO objDAO = new UsuarioDAO();
                tb_usuario objUser = objDAO.ValidarLogin(email_usuario, senha_usuario);

                if (objUser != null)
                {
                    CodigoUsuarioLogado = objUser.cod_usuario;
                    NomeUsuarioLogado = objUser.nome_usuario;
                    Response.Redirect("/Movimento/Movimento", false);
                    
                }
                else
                {
                    ViewBag.Validar = -1;
                }

            }
            return View("Login");
        }

    }
}
