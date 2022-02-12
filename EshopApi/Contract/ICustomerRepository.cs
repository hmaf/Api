using EshopApi.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EshopApi.Contract
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetList();
        Task<Customer> GetById(int id);
        Task<Customer> Add(Customer customer);
        Task<Customer> Update(Customer customer);
        Task<Customer> Delete(int id);
        Task<bool> IsExists(int id);
        Task<int> CountCustomer();
    }
}
