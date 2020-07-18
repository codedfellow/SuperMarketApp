using SupermarketApp.Interfaces;
using SupermarketApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApp.Repository
{
    public class SalesRepository : ISalesRepository
    {
        private readonly AppDbContext _db;
        public SalesRepository(AppDbContext context)
        {
            _db = context;
        }
        public bool Create(Sales entity)
        {
            _db.Sales.Add(entity);
            return Save();
        }

        public bool Delete(Sales entity)
        {
            _db.Sales.Remove(entity);
            return Save();
        }

        public ICollection<Sales> FindAll()
        {
            var Sales = _db.Sales.ToList();
            return Sales;
        }

        public Sales FindById(int id)
        {
            var order = _db.Sales.Find(id);
            return order;
        }

        public Sales GetLastSale()
        {
            var lastSale = _db.Sales.ToList().Last();
            return lastSale;
        }

        public bool IsExist(int id)
        {
            var exists = _db.Sales.Any(q => q.Id == id);
            return exists;
        }

        public bool Save()
        {
            var changes = _db.SaveChanges();
            return changes > 0;
        }

        public bool Update(Sales entity)
        {
            _db.Sales.Update(entity);
            return Save();
        }
    }
}
