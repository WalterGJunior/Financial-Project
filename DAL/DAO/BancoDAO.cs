using DAL.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BancoDAO
    {
        //Método para inserir Banco
        public void InserirBanco(tb_banco obj_Banco)
        {
            //Cria o Banco
            Banco objBanco = new Banco();

            //Adciona o objeto para gravar
            objBanco.AddTotb_banco(obj_Banco);

            //salva a operacao
            objBanco.SaveChanges();

        }

        //Método que será utilizado para criar uma lista com todos os Bancos cadastrados 
        public List<BancoVO> ConsultarBanco(int codusuario)
        {
            //Cria objeto Banco
            Banco objBanco = new Banco();

            //Será criada uma lista com nome de todos os Bancos e com seus respectivos endereços
            List<tb_banco> lstRetorno = objBanco.tb_banco.Include("tb_endereco")
                                        .Where(c => c.cod_usuario == codusuario).ToList();

            List<BancoVO> lstTratada = new List<BancoVO>();

            //For onde vai retornar todos os bancos e os endereço cadastrados e adcionar na lista (lstTratada)
            for (int i = 0; i < lstRetorno.Count; i++)
            {
                BancoVO objBancoVO = new BancoVO();

                //Guarda os valores do banco no objeto ObjBancoVO 
                objBancoVO.CodigoBanco = lstRetorno[i].cod_banco;
                objBancoVO.NomeBanco = lstRetorno[i].nome_banco;
                objBancoVO.Endereco = lstRetorno[i].tb_endereco.First().endereco;
                objBancoVO.Telefone = lstRetorno[i].tb_endereco.First().telefone;
                objBancoVO.SiteBanco = lstRetorno[i].site_banco;

                lstTratada.Add(objBancoVO);
            }

            return lstTratada;            
        }

        //Método para alterar todos os Bancos cadastrados
        public void AlterarBanco(tb_banco objEntrada)
        {
            //Cria Objeto Banco
            Banco objBanco = new Banco();

            //Será criado o objeto objCadastroBanco com o nome de todos os bancos cadastraddos e seus respectivos endereços
            tb_banco objCadastroBanco = objBanco.tb_banco.Include("tb_endereco")
                                        .FirstOrDefault(p => p.cod_banco == objEntrada.cod_banco);

            //Guarda os valores do banco no objeto ObjCadastroBanco a qual vao estar disponível para alteração
            objCadastroBanco.nome_banco = objEntrada.nome_banco;
            objCadastroBanco.tb_endereco.First().endereco = objEntrada.tb_endereco.First().endereco;
            objCadastroBanco.tb_endereco.First().endereco = objEntrada.tb_endereco.First().telefone;
            objCadastroBanco.site_banco = objEntrada.site_banco;

            //salva a operacao
            objBanco.SaveChanges();
        
        }
    }


}
