using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemData
{
    public class Utils
    {
        public ConsoleColor ColorSelect(int count)
        {
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            int[] color_index = new int[] { 14, 13, 12, 11, 10, 9, 6, 5, 4, 3, 2, 1 };
            return colors[color_index[count]];
        }
    }
}
