using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDNJR.DW.TCC.Domain.Entidades;

namespace WDNJR.DW.TCC.Domain.Repository.Configuration
{
    public class FuncionarioConfiguration : PessoaConfiguration
    {
        public FuncionarioConfiguration()
        {
            Map<Funcionario>(c => c.Requires("TipoPessoa").HasValue("F"));         
        }
    }
}
