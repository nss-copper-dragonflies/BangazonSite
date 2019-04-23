using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.ProductViewModels
{
    public class ProductTypesViewModel
    {
        public ProductType ProductType { get; set; }
        public Product Product { get; set; }
        public List<GroupedProducts>  GroupedProducts {get; set;}
    }
}
