using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAPI.ProductFolder
{
    internal class ProductResponse
    {
        public int responseCode { get; set; }

        public List<Product> products { get; set; }
    }
}
