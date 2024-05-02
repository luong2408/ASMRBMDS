using System;
using ASMWorkingWithRDBMS.Interface;
using MySql.Data.MySqlClient;

namespace ASMWorkingWithRDBMS.Implement
{
    public class Product : IProduct
    {
        public int productId { get; set; }
        public string name { get; set; }
        public int categoryId { get; set; }
        public float price { get; set; }

        public void AddProduct(MySqlConnection connection)
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
            string query = "INSERT INTO Product (ProductName, Category, Price) VALUES (@ProductName, @Category, @Price)";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProductName", productName);
                command.Parameters.AddWithValue("@Category", idCategory);
                command.Parameters.AddWithValue("@Price", price);
                command.ExecuteNonQuery();
            }
        }

        public void FindByCategory(MySqlConnection connection)
        {
            Console.Write("Enter category ID: ");
            int categoryId;
            if (!int.TryParse(Console.ReadLine(), out categoryId))
            {
                Console.WriteLine("Invalid category ID!");
                return;
            }

            string query = "SELECT * FROM Products WHERE CategoryID = @CategoryID";
            using (MySqlCommand categoryCommand = new MySqlCommand(query, connection))
            {
                categoryCommand.Parameters.AddWithValue("@CategoryID", categoryId);
                using (MySqlDataReader reader = categoryCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Product ID: {reader["ProductID"]}, Product Name: {reader["ProductName"]}, Category ID: {categoryId}, Price: {reader["Price"]}");
                    }
                }
            }
        }

        public void FindByName(MySqlConnection connection)
        {
            Console.Write("Enter product name: ");
            string productName = Console.ReadLine();
            string query = "SELECT * FROM Products WHERE ProductName LIKE CONCAT('%', @ProductName, '%')";
            using (MySqlCommand nameCommand = new MySqlCommand(query, connection))
            {
                nameCommand.Parameters.AddWithValue("@ProductName", productName);
                using (MySqlDataReader reader = nameCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Product ID: {reader["ProductID"]}, CategoryId: {reader["CategoryId"]}, Product Name: {reader["ProductName"]}, Price: {reader["Price"]}");
                    }
                }
            }
        }

        public void FindByPrice(MySqlConnection connection)
        {
            Console.Write("Enter minimum price: ");
            decimal minPrice = decimal.Parse(Console.ReadLine());
            Console.Write("Enter maximum price: ");
            decimal maxPrice = decimal.Parse(Console.ReadLine());
            string query = "SELECT * FROM Products WHERE Price >= @MinPrice AND Price <= @MaxPrice";
            using (MySqlCommand priceCommand = new MySqlCommand(query, connection))
            {
                priceCommand.Parameters.AddWithValue("@MinPrice", minPrice);
                priceCommand.Parameters.AddWithValue("@MaxPrice", maxPrice);
                using (MySqlDataReader reader = priceCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Product ID: {reader["ProductID"]}, CategoryId: {reader["CategoryId"]}, Product Name: {reader["ProductName"]}, Price: {reader["Price"]}");
                    }
                }
            }
        }

        public void UpdateProduct(MySqlConnection connection)
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
            string query = "UPDATE Products SET ProductName = @ProductName, Price = @Price WHERE ProductID = @ProductID";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProductName", productName);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@ProductID", productId);

                int numberOfLines = command.ExecuteNonQuery();
                Console.WriteLine($"{numberOfLines} Line has been updated.");
            }
        }

        private static bool ProductExists(MySqlConnection connection, int productId)
        {
            string query = "SELECT COUNT(*) FROM Products WHERE ProductID = @ProductID";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProductID", productId);

                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }
    }
}
