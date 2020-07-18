using SupermarketApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApp.Interfaces
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        bool RemoveAll();
    }
}
