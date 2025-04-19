﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Entities;
using Application.Core.Interfaces.Repositories;
using Infraestructure.Data;

namespace Infraestructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly VrContext _context;

        public ProdutoRepository(VrContext context) 
        {
            _context = context;
        }

        //Metodos aqui
    }
}
