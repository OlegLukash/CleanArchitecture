using System.Data;

namespace OnlineBookShop.Application.Common.Interfaces
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}
