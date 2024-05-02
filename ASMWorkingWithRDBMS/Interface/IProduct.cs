
using MySql.Data.MySqlClient;

namespace ASMWorkingWithRDBMS.Interface
{
    public interface IProduct
    {
        public void AddProduct(MySqlConnection connection);
        public void UpdateProduct(MySqlConnection connection);
        public void FindByCategory(MySqlConnection connection);
        public void FindByName(MySqlConnection connection);
        public void FindByPrice(MySqlConnection connection);
    }
}
