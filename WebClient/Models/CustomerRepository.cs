using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace WebClient.Models
{
    public class CustomerRepository
    {
        private string apiUrl = "http://localhost:22718/api/customers";
        private HttpClient _client;
        public CustomerRepository()
        {
            _client = new HttpClient();
        }

        public List<Customer> GetCustomerList(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = _client.GetStringAsync(apiUrl).Result;
            List<Customer> list = JsonConvert.DeserializeObject<List<Customer>>(result);

            return list;
        }

        public Customer GetCustomerById(int customerID)
        {
            var result = _client.GetStringAsync(apiUrl + "/" + customerID).Result;
            Customer customer = JsonConvert.DeserializeObject<Customer>(result);

            return customer;
        }

        public void AddCustomer(Customer customer)
        {
            var jsonCustoemr = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(jsonCustoemr, Encoding.UTF8, "application/json");

            var res = _client.PostAsync(apiUrl, content).Result;
        }

        public void UpdateCustomre(Customer customer)
        {
            var JsonCustomer = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(JsonCustomer, Encoding.UTF8, "application/json");

            var res = _client.PutAsync(apiUrl+"/"+customer.CustomerId, content).Result;
        }

        public void DeleteCustomer(int customerId, string token)
        {
            
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var res= _client.DeleteAsync(apiUrl + "/" + customerId).Result;
        }
        
    }
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

    }
}
