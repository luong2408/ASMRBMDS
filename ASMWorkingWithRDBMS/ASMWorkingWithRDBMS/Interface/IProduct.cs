
namespace ASMWorkingWithRDBMS.Interface
{
    public interface IProduct
    {
        public void AddProduct(SqlConnection connection);
        public void UpdateProduct(SqlConnection connection);
        public void FindByCategory(SqlConnection connection);
        public void FindByName(SqlConnection connection);
        public void FindByPrice(SqlConnection connection);
    }
}
