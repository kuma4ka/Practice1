using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardening.Logic.Utils
{
    public class LineSeparator
    {
        public void Separate(char separator)
        {
            switch(separator)
            {
                case '*':
                    Console.WriteLine("***********************");
                    break;
                default:
                    Console.WriteLine("-----------------------");
                    break;
            }
        }
    }
}
