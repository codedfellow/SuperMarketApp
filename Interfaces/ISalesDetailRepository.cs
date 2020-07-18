using SupermarketApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApp.Interfaces
{
    public interface ISalesDetailRepository : IBaseRepository<SalesDetail>
    {
        ICollection<SalesDetail> GetSalesDetail(int saleId);
    }
}
