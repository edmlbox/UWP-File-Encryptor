using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App46
{
    class StringManipulation
    {
        public static string subString(string x, char _char)
        {
            string temp = x;
            int index = temp.LastIndexOf(_char);
            if (index > 0) { temp = temp.Substring(0, index); }
            return temp;

        }

    }
}
