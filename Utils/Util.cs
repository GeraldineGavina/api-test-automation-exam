using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAPI.Utils
{
    public class Util
    {
        public static string GenerateCurrentDateAndTime()
        {
            return DateTime.Now.ToString("ddMMyyyyHHmmssfff");
        }
    }
}
