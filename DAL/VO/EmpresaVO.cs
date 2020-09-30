using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EmpresaVO
    {
        // Todos os campos que estão na tabela empresa
        public int CodigoEmpresa { get; set; }
        public string NomeEmpresa { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
    }
}
