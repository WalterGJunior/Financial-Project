using DAL.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DAL.DAO
{
    public class MovimentoDAO
    {
        public void RealizarEntrada(tb_movimento objEntrada)
        {
            using (TransactionScope objTran = new TransactionScope())
            {
                Banco objBanco = new Banco();

                objBanco.AddTotb_movimento(objEntrada);

                objBanco.SaveChanges();

                tb_conta objConta = objBanco.tb_conta.FirstOrDefault(p => p.cod_conta == objEntrada.cod_conta);
                objConta.saldo_conta = objConta.saldo_conta + objEntrada.valor_movimento;

                objBanco.SaveChanges();

                objTran.Complete();
            }
        }

        public void RealizarSaida(tb_movimento objSaida)
        {
            using (TransactionScope objTran = new TransactionScope())
            {
                Banco objBanco = new Banco();

                objBanco.AddTotb_movimento(objSaida);

                objBanco.SaveChanges();

                tb_conta objConta = objBanco.tb_conta.FirstOrDefault(p => p.cod_conta == objSaida.cod_conta);
                objConta.saldo_conta = objConta.saldo_conta - objSaida.valor_movimento;

                objBanco.SaveChanges();

                objTran.Complete();
            }
        }

        public List<MovimentoVO> RealizarConsulta(int cod_usuario, string tipo_movimento, string cod_categoria,
                                                    string data_entrada, string data_saida)
        { 
            Banco objBanco = new Banco();            
            List<MovimentoVO> lstRetorno = new List<MovimentoVO>();
                   

            List<tb_movimento> lstConsulta = new List<tb_movimento>();

            DateTime dtInicial = Convert.ToDateTime(data_entrada);
            DateTime dtFinal = Convert.ToDateTime(data_saida);
            Int16 tipo;
            int categoria;
 
            if (tipo_movimento == string.Empty)
            {
                if (cod_categoria == string.Empty)
                {
                    lstConsulta = objBanco.tb_movimento.Include("tb_conta").Include("tb_empresa")
                                  .Include("tb_categoria").Where(p => p.cod_usuario == cod_usuario &&
                                   p.data_movimento >= dtInicial &&
                                   p.data_movimento <= dtFinal).ToList();                            
                }
                else
                {
                    categoria = Convert.ToInt32(cod_categoria);
                    lstConsulta = objBanco.tb_movimento.Include("tb_conta").Include("tb_empresa")
                                  .Include("tb_categoria").Where(p => p.cod_usuario == cod_usuario &&
                                   p.data_movimento >= dtInicial &&
                                   p.data_movimento <= dtFinal &&
                                   p.cod_categoria == categoria ).ToList(); 
                }
            }
            else
            {
                tipo = Convert.ToInt16(tipo_movimento);
                if (cod_categoria == string.Empty)
                {
                    lstConsulta = objBanco.tb_movimento.Include("tb_conta").Include("tb_empresa")
                                  .Include("tb_categoria").Where(p => p.cod_usuario == cod_usuario &&
                                   p.data_movimento >= dtInicial &&
                                   p.data_movimento <= dtFinal &&
                                   p.tipo_movimento == tipo).ToList(); 
                }
                else
                {
                    categoria = Convert.ToInt32(cod_categoria);
                    lstConsulta = objBanco.tb_movimento.Include("tb_conta").Include("tb_empresa")
                                      .Include("tb_categoria").Where(p => p.cod_usuario == cod_usuario &&
                                       p.data_movimento >= dtInicial &&
                                       p.data_movimento <= dtFinal &&
                                       p.tipo_movimento == tipo &&
                                       p.cod_categoria == categoria).ToList();
                }
            }
            for (int i = 0; i < lstConsulta.Count; i++)
            {
                MovimentoVO objMov = new MovimentoVO();

                objMov.DataMovimento = lstConsulta[i].data_movimento.ToShortDateString();
                objMov.NomeCategoria = lstConsulta[i].tb_categoria.nome_categoria;
                objMov.NomeEmpresa = lstConsulta[i].tb_empresa.nome_empresa;
                objMov.NumeroConta = lstConsulta[i].tb_conta.numero_conta;
                objMov.ObsMovimento = lstConsulta[i].obs_movimento;
                objMov.ValorMovimento = lstConsulta[i].valor_movimento.ToString();

                if (lstConsulta[i].tipo_movimento == 1)
                {
                    objMov.TipoMovimento = "Entrada";
                }
                else
                {
                    objMov.TipoMovimento = "Saída";
                }

                lstRetorno.Add(objMov);
            }
            return lstRetorno;
        }

    }
}
