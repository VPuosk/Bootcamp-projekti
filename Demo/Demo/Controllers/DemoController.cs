using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            ViewData["laskuri"] = 20;
            return View();
        }

        //GET
        public IActionResult Laskuri()
        {
            //ViewData["laskuri"] = 20;
            return View();
        }

        //POST
        [HttpPost]
        public IActionResult Laskuri(Models.Arvopari arvopari)
        {
            //Console.WriteLine(arvopari.A);
            //Console.WriteLine(arvopari.B);
            ViewData["laskuri"] = arvopari.Laske();
            return View();
        }
    }
}
