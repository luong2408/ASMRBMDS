
using ASMWorkingWithRDBMS.Interface;

namespace ASMWorkingWithRDBMS.Implement
{
    public class Product : IProduct
    {
        int productId { get; set; }
        string name { get; set; }
        int categoryId {  get; set; }

        float price { get; set; }

        public void AddProduct(SqlConnection connection)
        {
            Console.WriteLine("Add product:");
            int idProduct = 0;
             do
            {
                string idCheck;                
                Console.Write("Enter ID Product (Number) : ");
                idCheck = Console.ReadLine();
                if (int.TryParse(idCheck, out idProduct))
                {                  
                    break;
                }
                else Console.WriteLine("Id is not Valid (id must be numeric) .");
            } while (true);
            int idCategory = 0;
            do
            {
                string idCheck;                
                Console.Write("Enter ID Category (Number) : ");
                idCheck = Console.ReadLine();
                if (int.TryParse(idCheck, out idCategory))
                {              
                    break;
                }
                else Console.WriteLine("Id is not Valid (id must be numeric) .");
            } while (true);
            Console.Write("Product Name: ");
            string productName = Console.ReadLine();
            decimal price = 0;
            do
            {
                string check;
                Console.Write("Enter price : ");
                check = Console.ReadLine();
                if (!decimal.TryParse(check, out price))
                {
                    Console.WriteLine("price Isvalid");
                }
                else
                {
                    break;
                }
            } while (true);
            string querry = "INSERT INTO Products (ProductName, Category, Price) VALUES (@ProductName, @Category, @Price)";
            SqlCommand command = new SqlCommand(querry, connection);
            command.Parameters.AddWithValue("@ProductID", idProduct);
            command.Parameters.AddWithValue("@CategoryId", idCategory);
            command.Parameters.AddWithValue("@ProductName", productName);
            command.Parameters.AddWithValue("@Price", price);
        }

        public void FindByCategory(SqlConnection connection)
        {
            Console.Write("Enter category ID: ");
            int categoryId;
            if (!int.TryParse(Console.ReadLine(), out categoryId))
            {
                Console.WriteLine("Invalid category ID!");
                return;
            }

            string query = "SELECT * FROM Products WHERE CategoryID = @CategoryID";
            using (SqlCommand categoryCommand = new SqlCommand(query, connection))
            {
                categoryCommand.Parameters.AddWithValue("@CategoryID", categoryId);
                using (SqlDataReader reader = categoryCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Product ID: {reader["ProductID"]}, Product Name: {reader["ProductName"]}, Category ID: {categoryId}, Price: {reader["Price"]}");
                    }
                }
            }
        }
        public void FindByName(SqlConnection connection)
        {
            Console.Write("Enter product name: ");
            string productName = Console.ReadLine();
            query = "SELECT * FROM Products WHERE ProductName LIKE '%' + @ProductName + '%'";
            SqlCommand nameCommand = new SqlCommand(query, connection);
            nameCommand.Parameters.AddWithValue("@ProductName", productName);
            using (SqlDataReader reader = nameCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Product ID: {reader["ProductID"]}, CategoryId: {reader["CategoryId"]}, Product Name: {reader["ProductName"]}, Price: {reader["Price"]}");
                }
            }
        }

        public void FindByPrice(SqlConnection connection)
        {
            Console.Write("Enter minimum price: ");
            decimal minPrice = decimal.Parse(Console.ReadLine());
            Console.Write("Enter maximum price: ");
            decimal maxPrice = decimal.Parse(Console.ReadLine());
            query = "SELECT * FROM Products WHERE Price >= @MinPrice AND Price <= @MaxPrice";
            SqlCommand priceCommand = new SqlCommand(query, connection);
            priceCommand.Parameters.AddWithValue("@MinPrice", minPrice);
            priceCommand.Parameters.AddWithValue("@MaxPrice", maxPrice);

            using (SqlDataReader reader = priceCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Product ID: {reader["ProductID"]},CategoryId: {reader["CategoryId"]},Product Name: {reader["ProductName"]}, Price: {reader["Price"]}");
                }
            }
        }

        public void UpdateProduct(SqlConnection connection)
        {
            int productId = 0;
            do
            {
                string idCheck;
                Console.Write("Enter ID Product (Number) : ");
                idCheck = Console.ReadLine();
                if (int.TryParse(idCheck, out productId))
                {
                    break;
                }
                else Console.WriteLine("Id is not Valid (id must be numeric) .");
            } while (true);
            if (!ProductExists(connection, productId))
            {
                Console.WriteLine("Product not found.");
                return;
            }
            Console.WriteLine("Update Product:");
            Console.Write("Product name: ");
            string productName = Console.ReadLine();
            decimal price;
            do
            {
                string check;
                Console.Write("Enter price : ");
                check = Console.ReadLine();
                if (!decimal.TryParse(check, out price))
                {
                    Console.WriteLine("price Isvalid");
                }
                else
                {
                    break;
                }
            } while (true);
            string querry = "UPDATE Products SET ProductName = @ProductName, Category = @Category, Price = @Price WHERE ProductID = @ProductID";
            SqlCommand command = new SqlCommand(querry, connection);
            command.Parameters.AddWithValue("@ProductName", productName);       
            command.Parameters.AddWithValue("@Price", price);
            command.Parameters.AddWithValue("@ProductID", productId);

            int numberOfLines = command.ExecuteNonQuery();
            Console.WriteLine($"{numberOfLines} Line has been updated.");
        }
        private static bool ProductExists(SqlConnection connection, int productId)
        {
            string query = "SELECT COUNT(*) FROM Products WHERE ProductID = @ProductID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ProductID", productId);

            int count = (int)command.ExecuteScalar();
            return count > 0;
        }
    } 
}
