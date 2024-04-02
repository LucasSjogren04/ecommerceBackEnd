using Microsoft.Data.SqlClient;

namespace ecommerceBackEnd.Repository.Interfaces
{
    public interface IDBContext
    {
        SqlConnection GetConnection();
    }
}
