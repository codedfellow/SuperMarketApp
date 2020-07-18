using SupermarketApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApp.Interfaces
{
    public interface ISalesRepository : IBaseRepository<Sales>
    {
        Sales GetLastSale();
    }
}
