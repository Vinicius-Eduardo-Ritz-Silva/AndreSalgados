using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Entities;

namespace Application.Core.DTOs
{
    public class RelatorioVendaDTO
    {
        public decimal Valor { get; set; }

        public bool VendaGanha { get; set; }

        public DateTime Data { get; set; }

        public string NomeCliente { get; set; }
    }
}
