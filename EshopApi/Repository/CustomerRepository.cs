using EshopApi.Contract;
using EshopApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopApi.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private EshopApi_DBContext _context;
        private IMemoryCache _cache;

        public CustomerRepository(EshopApi_DBContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<Customer> Add(Customer customer)
        {
            await _context.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<int> CountCustomer()
        {
            return await _context.Customers.CountAsync();
        }

        public async Task<Customer> Delete(int id)
        {
             var customer = await _context.Customers.FindAsync(id);
             _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> GetById(int id)
        {
            var cacheCustomer = _cache.Get<Customer>(id);
            if (cacheCustomer != null)
            {
                return cacheCustomer;
            }
            else
            {
                var customer = await _context.Customers.Include(c => c.Orders).SingleOrDefaultAsync(c => c.CustomerId == id);
                var cacheOption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));
                _cache.Set(customer.CustomerId, customer, cacheOption);
                return customer;
            }
            
        }

        public IEnumerable<Customer> GetList()
        {
            return _context.Customers.ToList();
        }

        public async Task<bool> IsExists(int id)
        {
            return await _context.Customers.AnyAsync(c=>c.CustomerId == id);
        }

        public async Task<Customer> Update(Customer customer)
        {
            _context.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
    }
}
