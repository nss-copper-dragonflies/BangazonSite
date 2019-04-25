using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.OrderViewModels
{
    public class OrderProductsViewModel
    {
       public List<Product> product { get; set; }
       public Order order { get; set; }
        public Order LineItems { get; internal set; }
    }
}
