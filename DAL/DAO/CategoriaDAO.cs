using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CategoriaDAO
    {
        //Método para Inserir  Categoria
        public void InserirCategoria(tb_categoria objEntrada)
        {
            //Cria o Banco
            Banco objBanco = new Banco();

            //Adciona o objeto para gravar
            objBanco.AddTotb_categoria(objEntrada);

            //salva a operacao
            objBanco.SaveChanges();

        }

        //Método que será utilizado para criar uma lista com todos as categorias cadastradas 
        public List<tb_categoria> ConsultarCategoria(int codusuario)
        {
            //Cria o Banco
            Banco objbanco = new Banco();

            //Será criada uma lista com nome de todas as Categorias e armazenada no objeto lstRetorno
            List<tb_categoria> lstRetorno = objbanco.tb_categoria.Where(c => c.cod_usuario == codusuario).ToList();

            return lstRetorno;
        }

        //Método para alterar todas as Categorias cadastradas
        public void AlterarCategoria(tb_categoria objEntrada)
        {
            //Cria o Banco
            Banco objBanco = new Banco();

            //Cria o objResgate onde será armazenado todos as categorias cadastradas no Banco onde as mesma vão estar disponivel para Alteração
            tb_categoria objResgate = objBanco.tb_categoria.FirstOrDefault(p => p.cod_categoria == objEntrada.cod_categoria);

            //O objResgate deve ser o mesmo do ObjEntrada (Pois veridica se o valor a ser alterado na interface é o mesmo que está no banco) 
            objResgate.nome_categoria = objEntrada.nome_categoria;

            //Salva a Operação
            objBanco.SaveChanges();

        }
    }
}