using ASMWorkingWithRDBMS;
using ASMWorkingWithRDBMS.Implement;
Product product = new Product();
menu menu=new menu();
string chuoiKetNoi = "Chuỗi_Kết_Nối_Của_Bạn";
using (SqlConnection connection = new SqlConnection(chuoiKetNoi))
{
    connection.Open();
    Console.WriteLine("connected to the database");
    int choice = 0;
    string check;
    while (choice != 4)
    {
        menu.ProductMenu();
        do
        {
            Console.Write("Select System: ");
            check = Console.ReadLine();
            if (!int.TryParse(check, out choice))
            {
                Console.WriteLine("Selection Isvalid");
            }
            else break;
        } while (true);
        switch (choice)
        {
            case 1:
                {
                    product.AddProduct(connection);
                    break;
                }
            case 2:
                {
                    product.UpdateProduct(connection);
                    break;
                }
            case 3:
                {
                    Console.Write("Select System: ");
                    check = Console.ReadLine();
                    if (!int.TryParse(check, out choice))
                    {
                        Console.WriteLine("Selection Isvalid");
                    }
                    menu.FindMenu();
                    do
                    {
                        Console.Write("Select System: ");
                        check = Console.ReadLine();
                        if (!int.TryParse(check, out choice))
                        {
                            Console.WriteLine("Selection Isvalid");
                        }
                        else break;
                    } while (true);
                    switch (choice)
                    {
                        case 1:
                            {
                                product.FindByCategory(connection);
                                break;
                            }
                        case 2:
                            {
                                product.FindByName(connection); ;
                                break;
                            }
                        case 3:
                            {
                                product.FindByPrice(connection);
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Selection isvalid");
                                break;
                            }
                    }
                    break;
                }
            case 4:
                {
                    Console.WriteLine("Exit");
                    return;
                }
            default:
                {
                    Console.WriteLine("Selection is valid.");
                    break;
                }
        }
    }
}