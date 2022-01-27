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
    public class ClienteService : IClienteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClienteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Cliente> Get()
        {
            var result = _unitOfWork.ClienteRepository.Select();

            return result;
        }

        public Cliente GetById(Int64 id)
        {
            return _unitOfWork.ClienteRepository.SelectById(id);
        }

        public bool Post(Cliente cliente)
        {
            try
            {     
                _unitOfWork.BeginTransaction();

                _unitOfWork.ClienteRepository.Insert(cliente);               

                _unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        public bool Put(Cliente cliente)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                Cliente clienteSelect =  _unitOfWork.ClienteRepository.SelectById(cliente.IdCliente);

                cliente.Cpf = clienteSelect.Cpf;

                _unitOfWork.ClienteRepository.Update(cliente);

                _unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        public bool Delete(Int64 id)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                Cliente clienteSelect = _unitOfWork.ClienteRepository.SelectById(id);

                _unitOfWork.ClienteRepository.Delete(clienteSelect);

                _unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }
    }
}
