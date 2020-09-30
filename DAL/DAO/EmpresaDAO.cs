using DAL.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EmpresaDAO
    {
        public void InserirEmpresa(tb_empresa objEmpresa)
        {
            //Cria o Banco
            Banco objBanco = new Banco();

            //Adciona o objeto para gravar
            objBanco.AddTotb_empresa(objEmpresa);
            
            //salva a operacao
            objBanco.SaveChanges();

        }

        public List<EmpresaVO> ConsultarEmpresa(int codusuario)
        {

            Banco objbanco = new Banco();
            List<tb_empresa> lstRetorno = objbanco.tb_empresa.Include("tb_endereco")
                            .Where(c => c.cod_usuario == codusuario).ToList();


            List<EmpresaVO> lstTratada = new List<EmpresaVO>();

            for (int i = 0; i < lstRetorno.Count; i++)
            {
                EmpresaVO objEmpresaVO = new EmpresaVO();

                objEmpresaVO.CodigoEmpresa = lstRetorno[i].cod_empresa;
                objEmpresaVO.NomeEmpresa = lstRetorno[i].nome_empresa;
                objEmpresaVO.Endereco = lstRetorno[i].tb_endereco.First().endereco;
                objEmpresaVO.Telefone = lstRetorno[i].tb_endereco.First().telefone;

                lstTratada.Add(objEmpresaVO);
            }

            return lstTratada;
        }

        public void AlterarEmpresa(tb_empresa objEntrada)
        {
            Banco objBanco = new Banco();

            tb_empresa objEmpresa = objBanco.tb_empresa.Include("tb_endereco")
                       .FirstOrDefault(p => p.cod_empresa == objEntrada.cod_empresa);

            objEmpresa.nome_empresa = objEntrada.nome_empresa;
            objEmpresa.tb_endereco.First().endereco = objEntrada.tb_endereco.First().endereco;
            objEmpresa.tb_endereco.First().endereco = objEntrada.tb_endereco.First().telefone;

            objBanco.SaveChanges();

        }
    }
}
