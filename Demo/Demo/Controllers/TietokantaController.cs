using Demo.ReitinhaunLuokat;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    public class TietokantaController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
