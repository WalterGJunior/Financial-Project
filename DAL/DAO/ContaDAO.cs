using DAL.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class ContaDAO
    {
        public void InserirConta(tb_conta objEntrada)
        {
            Banco objBanco = new Banco();
            objBanco.AddTotb_conta(objEntrada);
            objBanco.SaveChanges();
        }

        public void AlterarConta(tb_conta objEntrada)
        {
            Banco objBanco = new Banco();

            tb_conta objResgateConta = objBanco.tb_conta.FirstOrDefault(p => p.cod_conta == objEntrada.cod_conta);

            //Atualiza os Valores
            objResgateConta.numero_conta = objEntrada.numero_conta;
            objResgateConta.cod_banco = objEntrada.cod_banco;
            objResgateConta.status_conta = objEntrada.status_conta;
            objResgateConta.saldo_conta = objEntrada.saldo_conta;

            objBanco.SaveChanges();
        }

        public List<ContaVO> ConsultarConta(int codUser)
        {
            Banco objBanco = new Banco();
            List<ContaVO> lstRetorno = new List<ContaVO>(); 

            List<tb_conta> lstConta = objBanco.tb_conta.Include("tb_banco").Where(p => p.cod_usuario == codUser).ToList();

            for (int i = 0; i < lstConta.Count; i++)
            {
                ContaVO objContaVO = new ContaVO();

                objContaVO.CodigoConta = lstConta[i].cod_conta;
                objContaVO.CodigoBanco = lstConta[i].cod_banco;
                objContaVO.NomeBanco = lstConta[i].tb_banco.nome_banco;
                objContaVO.NumeroConta = lstConta[i].numero_conta;
                objContaVO.SaldoConta = lstConta[i].saldo_conta;
                objContaVO.StatusConta = lstConta[i].status_conta;

                lstRetorno.Add(objContaVO);
            }

            return lstRetorno;

        }

        public List<ContaVO> ConsultarContaAtiva(int codUser)
        {
            Banco objBanco = new Banco();
            List<ContaVO> lstRetorno = new List<ContaVO>();

            List<tb_conta> lstConta = objBanco.tb_conta.Include("tb_banco").Where(p => p.cod_usuario == codUser && p.status_conta == true).ToList();

            for (int i = 0; i < lstConta.Count; i++)
            {
                ContaVO objContaVO = new ContaVO();

                objContaVO.CodigoConta = lstConta[i].cod_conta;
                objContaVO.CodigoBanco = lstConta[i].cod_banco;
                objContaVO.NomeBanco = lstConta[i].tb_banco.nome_banco;
                objContaVO.NumeroConta = lstConta[i].numero_conta;
                objContaVO.SaldoConta = lstConta[i].saldo_conta;
                objContaVO.StatusConta = lstConta[i].status_conta;

                lstRetorno.Add(objContaVO);
            }

            return lstRetorno;

        }


    }
}
