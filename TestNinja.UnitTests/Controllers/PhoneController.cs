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
    }
}