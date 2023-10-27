using Gardening.Class;
using Gardening.Interface;

namespace Gardening.UI
{
    internal partial class Program
    {
        private static void Main(string[] args)
        {
            Plant.PrintGardeningInfo();
            Menu menu = new Menu();

            menu.MenuOptions();
        }
    }
}