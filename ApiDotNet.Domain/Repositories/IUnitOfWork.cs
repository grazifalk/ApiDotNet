namespace ApiDotNet.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable //implementar para garantir que caso haja transação aberta, vamos matá-la, por garantia
    {
        Task BeginTransaction();
        Task Commit();
        Task Rollback();
    }
}
