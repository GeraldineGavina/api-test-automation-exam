using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAPI.ProductFolder
{
    internal class BrandResponse
    {
        public int responseCode { get; set; }
        public List<Brand> brands { get; set; }

    }
}
