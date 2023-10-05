using APIClient.Data;
using APIClient.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIClient.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacaoController : Controller
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AvaliacaoController(IMapper mapper, DataContext context)
        {
            _mapper= mapper;
            _context = context;
        }
    }
}
