namespace ASMWorkingWithRDBMS
{
    public class Menu
    {
       public void ProductMenu()
        {
            Console.WriteLine("Product Menu");
            Console.WriteLine("1.Add product");
            Console.WriteLine("2.Update product");
            Console.WriteLine("3.Find product");
        }
        public void FindMenu()
        {
            Console.WriteLine("Menu find");
            Console.WriteLine("1.Find by category");
            Console.WriteLine("2.Find by name");
            Console.WriteLine("3.Find by price");
        }
    }
}
