using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class UsuarioDAO
    {
        public tb_usuario ValidarLogin(string email_usuario, string senha_usuario)
        {
            Banco objBanco = new Banco();

            tb_usuario objUser = objBanco.tb_usuario.FirstOrDefault(p => p.email_usuario == email_usuario && p.senha_usuario == senha_usuario);
            return objUser;
        }
    }
}
