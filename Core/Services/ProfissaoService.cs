using Core.Interfaces.Services;
using Domain.Entities;
using Infra.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shared.Extensions;
using System;
using System.Collections.Generic;

namespace Core.Services
{
    public class ProfissaoService : IProfissaoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProfissaoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Profissao> Get()
        {
            return _unitOfWork.ProfissaoRepository.Select();
        }

        public Profissao GetById(Int32 id)
        {
            return _unitOfWork.ProfissaoRepository.SelectById(id);
        }      
    }
}
