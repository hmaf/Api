using EshopApi.Contract;
using EshopApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopApi.Repository
{
    public class ProductsRepository : IProductRepository
    {
        private EshopApi_DBContext _context;

        public ProductsRepository(EshopApi_DBContext context)
        {
            _context = context;
        }

        public async Task<Product> Add(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Delete(int id)
        {
            var products = await _context.Products.SingleAsync(p=>p.ProductId == id);
             _context.Products.Remove(products);
             await _context.SaveChangesAsync();
            return products;
        }

        public async Task<Product> Edit(Product product)
        {
           _context.Products.Update(product);
           await _context.SaveChangesAsync();
            return product;
        }

        public IEnumerable<Product> GetAllProduct()
        {
            return _context.Products.ToList();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.SingleOrDefaultAsync(p => p.ProductId == id); ;
        }

        public  async Task<bool> IsExist(int id)
        {
            return await _context.Products.AnyAsync(p => p.ProductId == id);
        }
    }
}
