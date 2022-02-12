using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebClient.Models;

namespace WebClient.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private CustomerRepository _customer;
        public HomeController()
        {
            _customer=new CustomerRepository();
        }

        public IActionResult Index()
        {
            string token = User.FindFirst("AccessToken").Value;
            return View(_customer.GetCustomerList(token));
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            _customer.AddCustomer(customer);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var customer = _customer.GetCustomerById(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            _customer.UpdateCustomre(customer);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            string token = User.FindFirst("AccessToken").Value;
            _customer.DeleteCustomer(id, token);
            return RedirectToAction("Index");
        }
    }
}