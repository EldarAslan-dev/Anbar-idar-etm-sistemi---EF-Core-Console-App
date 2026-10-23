using EFCoreInventoryManager.Services;

namespace EFCoreInventoryManager
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            var app = new InventoryApp();
            app.Run();
        }
    }
}