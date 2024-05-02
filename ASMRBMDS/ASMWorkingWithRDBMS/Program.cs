using ASMWorkingWithRDBMS;
using ASMWorkingWithRDBMS.Implement;
using MySql.Data.MySqlClient;

namespace YourNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            string stringConnection = "server=localhost;port=3306;user=root;password=Huyat1529#;database=rmdbs_asignment";

            using (MySqlConnection connection = new MySqlConnection(stringConnection))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connected to MySQL database.");

                    Product product = new Product();
                    Menu menu = new Menu();

                    int choice = 0;
                    string check;
                    while (choice != 4)
                    {
                        menu.ProductMenu();
                        do
                        {
                            Console.Write("Select Option: ");
                            check = Console.ReadLine();
                            if (!int.TryParse(check, out choice))
                            {
                                Console.WriteLine("Invalid selection.");
                            }
                            else break;
                        } while (true);

                        switch (choice)
                        {
                            case 1:
                                product.AddProduct(connection);
                                break;
                            case 2:
                                product.UpdateProduct(connection);
                                break;
                            case 3:
                                menu.FindMenu();
                                do
                                {
                                    Console.Write("Select Option: ");
                                    check = Console.ReadLine();
                                    if (!int.TryParse(check, out choice))
                                    {
                                        Console.WriteLine("Invalid selection.");
                                    }
                                    else break;
                                } while (true);

                                switch (choice)
                                {
                                    case 1:
                                        product.FindByCategory(connection);
                                        break;
                                    case 2:
                                        product.FindByName(connection);
                                        break;
                                    case 3:
                                        product.FindByPrice(connection);
                                        break;
                                    default:
                                        Console.WriteLine("Invalid selection.");
                                        break;
                                }
                                break;
                            case 4:
                                Console.WriteLine("Exiting...");
                                break;
                            default:
                                Console.WriteLine("Invalid selection.");
                                break;
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
