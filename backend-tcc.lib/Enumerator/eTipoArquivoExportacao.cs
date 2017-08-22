using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_tcc.lib.Enumerator
{
    public enum eTipoArquivoExportacao : short
    {
        NaoAtribuido = 0,

        Pdf = 1,

        Docx = 2,

        Xlsx = 3,

        Txt = 4,

        Csv = 5,

        Zip = 6
    }
}
