using SupermarketApp.Interfaces;
using SupermarketApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApp.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _db;
        public CartRepository(AppDbContext context)
        {
            _db = context;
        }
        public bool Create(Cart entity)
        {
            _db.Cart.Add(entity);
            return Save();
        }

        public bool Delete(Cart entity)
        {
            _db.Cart.Remove(entity);
            return Save();
        }

        public ICollection<Cart> FindAll()
        {
            var Cart = _db.Cart.ToList();
            return Cart;
        }

        public Cart FindById(int id)
        {
            var Cart = _db.Cart.Find(id);
            return Cart;
        }

        public bool IsExist(int id)
        {
            var exists = _db.Cart.Any(q => q.Id == id);
            return exists;
        }

        public bool RemoveAll()
        {
            var allCartItems = FindAll().ToList();
            foreach (var item in allCartItems)
            {
                Delete(item);
            }
            return Save();
        }

        public bool Save()
        {
            var changes = _db.SaveChanges();
            return changes > 0;
        }

        public bool Update(Cart entity)
        {
            _db.Cart.Update(entity);
            return Save();
        }
    }
}
