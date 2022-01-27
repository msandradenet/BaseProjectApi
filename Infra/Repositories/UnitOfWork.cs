using Infra.Interfaces;
using System;
using System.Data.SqlClient;

namespace Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private SqlConnection _sqlConnection;
        private SqlTransaction _sqlTransaction;

        public IClienteRepository ClienteRepository { get; }
        public IProfissaoRepository ProfissaoRepository { get; }

        public UnitOfWork(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;

            ClienteRepository = new ClienteRepository(sqlConnection);
            ProfissaoRepository = new ProfissaoRepository(sqlConnection);
        }

        public void BeginTransaction()
        {
            _sqlTransaction = _sqlConnection.BeginTransaction();

            ClienteRepository.SqlTransaction = _sqlTransaction;
            ProfissaoRepository.SqlTransaction = _sqlTransaction;
        }

        public void Commit()
        {
            if (_sqlTransaction != null)
            {
                _sqlTransaction.Commit();
            }
        }

        public void Rollback()
        {
            if (_sqlTransaction != null)
            {
                _sqlTransaction.Rollback();
            }
        }

        public void Dispose()
        {
            if (_sqlTransaction != null)
            {
                _sqlTransaction.Dispose();
            }

            if (_sqlConnection != null)
            {
                _sqlConnection.Close();
                _sqlConnection.Dispose();
            }
        }
    }
}
