using SupermarketApp.Interfaces;
using SupermarketApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApp.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext context)
        {
            _db = context;
        }
        public bool Create(Product entity)
        {
            _db.Products.Add(entity);
            return Save();
        }

        public bool Delete(Product entity)
        {
            _db.Products.Remove(entity);
            return Save();
        }

        public ICollection<Product> FindAll()
        {
            var orders = _db.Products.ToList();
            return orders;
        }

        public Product FindById(int id)
        {
            var order = _db.Products.Find(id);
            return order;
        }

        public bool IsExist(int id)
        {
            var exists = _db.Products.Any(q => q.Id == id);
            return exists;
        }

        public bool Save()
        {
            var changes = _db.SaveChanges();
            return changes > 0;
        }

        public bool Update(Product entity)
        {
            _db.Products.Update(entity);
            return Save();
        }
    }
}
