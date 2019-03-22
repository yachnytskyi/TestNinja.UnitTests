using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestNinja.UnitTests.Models;

namespace TestNinja.UnitTests.Controllers
{
    public class PhoneController : Controller
    {
        IRepository repo;
        public PhoneController(IRepository r)
        {
            repo = r;
        }
        public IActionResult Index()
        {
            return View(repo.GetAll());
        }
        public IActionResult GetPhone(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            Phone phone = repo.Get(id.Value);
            if (phone == null)
            {
                return NotFound();
            }
            return View(phone);
        }

        public IActionResult AddPhone() => View();

        [HttpPost]
        public IActionResult AddPhone(Phone phone)
        {
            if (ModelState.IsValid)
            {
                repo.Create(phone);
                return RedirectToAction("Index");
            }
            return View(phone);
        }
    }
}