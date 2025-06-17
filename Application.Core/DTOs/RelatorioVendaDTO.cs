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
        public decimal PagoNaHora { get; set; }

        public decimal PagoEmDia { get; set; }

        public decimal PagoComAtraso { get; set; }

        public decimal NaoPago { get; set; }

        public DateTime Data { get; set; }
    }
}
