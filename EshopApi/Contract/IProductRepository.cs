using EshopApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EshopApi.Contract
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProduct();
        Task<Product> GetById(int id);
        Task<Product> Add(Product product);
        Task<Product> Edit(Product product);
        Task<Product> Delete(int id);
        Task<bool> IsExist(int id);
    }
}
