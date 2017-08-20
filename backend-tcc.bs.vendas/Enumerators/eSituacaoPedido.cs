using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_tcc.bs.vendas.Enumerators
{
    public enum eSituacaoPedido: short
    {
        NaoAtribuido = 0,
        Aberto = 1,
        Faturado = 2,
        Cancelado = 3
    }
}
