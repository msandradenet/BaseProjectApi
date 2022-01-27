using System.Collections.Generic;
using Domain.Models;
using Domain.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace API.Controllers
{
    [ApiVersion("1")]
    public class ProfissaoController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IProfissaoService _service;
        public ProfissaoController(ILogger<BaseController> logger, IHttpContextAccessor httpContextAccessor, IMapper mapper, IProfissaoService service) : base(logger, httpContextAccessor)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public ActionResult<BaseResponse<IEnumerable<ProfissaoResponse>>> Get()
        {
            return Execute(() =>
            {
                IEnumerable<Profissao> Profissao = _service.Get();

                return _mapper.Map<IEnumerable<ProfissaoResponse>>(Profissao);
            });
        }

        [HttpGet("{id}")]
        public ActionResult<BaseResponse<ProfissaoResponse>> GetById([FromRoute] int id)
        {
            return Execute(() =>
            {
                Profissao Profissao = _service.GetById(id);

                return _mapper.Map<ProfissaoResponse>(Profissao);
            });
        }
    }
}