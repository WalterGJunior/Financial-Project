using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.VO
{
    public class MovimentoVO
    {
        public string TipoMovimento { get; set; }
        public string DataMovimento { get; set; }
        public string ValorMovimento { get; set; }
        public string NumeroConta { get; set; }
        public string NomeCategoria { get; set; }
        public string NomeEmpresa { get; set; }
        public string ObsMovimento { get; set; }
    }
}
