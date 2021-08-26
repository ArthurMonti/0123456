using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.Models
{
    public class Despesa
    {

        public int Codigo { get; set; }
        public Tipo_Despesa Tipo_despesa { get; set; }
        public String Descricao { get; set; }
        public int QtdeParcelas { get; set; }
        public decimal valor_total {get;set;}
        public DateTime Data_despesa { get; set; }

    }
}
