using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Entities;
using Application.Core.Interfaces.Repositories;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class ClienteRepository : MainRepository<Cliente>, IClienteRepository
    {
        private readonly VrContext _context;

        public ClienteRepository(VrContext context) : base(context)
        {
            _context = context;
        }

    }
}
