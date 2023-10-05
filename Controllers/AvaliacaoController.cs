using APIClient.Data;
using APIClient.Interfaces;
using APIClient.Models;
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

        private readonly IMapper _mapper;
        public AvaliacaoController(IMapper mapper)
        {
            _mapper= mapper;
        }
    }
}
