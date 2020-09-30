using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1 
{
    public class PageBase : Controller
    {
        public int CodigoUsuarioLogado
        {
            get
            {
                if (Session["codUser"] == null)
                {
                    Session["codUser"] = 0;
                }
                return (int)Session["codUser"];
                
            }
            set 
            {
                Session["codUser"] = value;
            }
        }

        public string NomeUsuarioLogado
        {
            get
            {
                if (Session["nomeUser"] == null)
                {
                    Session["nomeUser"] = string.Empty;
                }
                return (string)Session["nomeUser"];

            }
            set
            {
                Session["nomeUser"] = value;
            }
        }
    }
}