namespace Infra.Interfaces
{
    public interface IUnitOfWork
    {
        IClienteRepository ClienteRepository { get; }
        IProfissaoRepository ProfissaoRepository { get; }   

        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
