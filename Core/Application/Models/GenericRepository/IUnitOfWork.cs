using System.Data;

namespace Application.Models.GenericRepository;

public interface IUnitOfWork : IDisposable
{
    IDbConnection Connection { get; }

    IDbTransaction Transaction { get; }
}
