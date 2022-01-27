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
    public class ClienteController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IClienteService _service;
        public ClienteController(ILogger<BaseController> logger, IHttpContextAccessor httpContextAccessor, IMapper mapper, IClienteService service) : base(logger, httpContextAccessor)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public ActionResult<BaseResponse<IEnumerable<ClienteResponse>>> Get()
        {
            return Execute(() =>
            {
                IEnumerable<Cliente> Cliente = _service.Get();

                return _mapper.Map<IEnumerable<ClienteResponse>>(Cliente);
            });
        }

        [HttpGet("{id}")]
        public ActionResult<BaseResponse<ClienteResponse>> GetById([FromRoute]int id)
        {
            return Execute(() =>
            {
                Cliente Cliente = _service.GetById(id);

                return _mapper.Map<ClienteResponse>(Cliente);
            });
        }

        [HttpPost]
        public ActionResult<BaseResponse<bool>> Post([FromBody] ClienteRequest request)
        {            
            return Execute(() =>
            {
                Cliente Cliente = _mapper.Map<Cliente>(request);

                return _service.Post(Cliente);
            });
        }

        [HttpPut]
        public ActionResult<BaseResponse<bool>> Put([FromBody] ClienteRequest request)
        {
            return Execute(() =>
            {
                Cliente Cliente = _mapper.Map<Cliente>(request);

                return _service.Put(Cliente);
            });
        }

        [HttpDelete("{id}")]
        public ActionResult<BaseResponse<bool>> Delete([FromRoute] int id)
        {
            return Execute(() =>
            {
                return _service.Delete(id);
            });
        }
    }
}