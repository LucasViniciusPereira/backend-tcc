﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_tcc.bs.marketing.Class
{
    public class Propaganda
    {
        public int PropagandaID { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Observacoes { get; set; }
    }
}