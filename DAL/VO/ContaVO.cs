using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.VO
{
    public class ContaVO
    {
        public int CodigoConta { get; set; }
        public string NumeroConta { get; set; }
        public decimal SaldoConta { get; set; }
        public bool StatusConta { get; set; }
        public int CodigoBanco { get; set; }
        public string NomeBanco { get; set; }
    }
}
