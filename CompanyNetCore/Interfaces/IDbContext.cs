using System.Data;

namespace CompanyNetCore.Interfaces
{
    public interface IDbContext
    {
        IDbConnection GetCompany();
    }
}
