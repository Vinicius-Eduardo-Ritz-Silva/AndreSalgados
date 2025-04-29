using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.DTOs
{
    public class ResultadoDTO
    {
        public bool Sucesso { get; set; }

        public string Mensagem { get; set; }

        public object Dados { get; set; }

        public string Codigo { get; set; }
    }
}
