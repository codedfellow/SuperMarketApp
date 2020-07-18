using SupermarketApp.Interfaces;
using SupermarketApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApp.Repository
{
    public class SalesDetailRepository : ISalesDetailRepository
    {
        private readonly AppDbContext _db;
        public SalesDetailRepository(AppDbContext context)
        {
            _db = context;
        }
        public bool Create(SalesDetail entity)
        {
            _db.SalesDetails.Add(entity);
            return Save();
        }

        public bool Delete(SalesDetail entity)
        {
            _db.SalesDetails.Remove(entity);
            return Save();
        }

        public ICollection<SalesDetail> FindAll()
        {
            var orders = _db.SalesDetails.ToList();
            return orders;
        }

        public SalesDetail FindById(int id)
        {
            var order = _db.SalesDetails.Find(id);
            return order;
        }

        public ICollection<SalesDetail> GetSalesDetail(int saleId)
        {
            return FindAll().Where(p => p.SalesId == saleId).ToList();
        }

        public bool IsExist(int id)
        {
            var exists = _db.SalesDetails.Any(q => q.Id == id);
            return exists;
        }

        public bool Save()
        {
            var changes = _db.SaveChanges();
            return changes > 0;
        }

        public bool Update(SalesDetail entity)
        {
            _db.SalesDetails.Update(entity);
            return Save();
        }
    }
}
