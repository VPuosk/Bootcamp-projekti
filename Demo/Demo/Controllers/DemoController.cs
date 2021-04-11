﻿using Microsoft.AspNetCore.Mvc;
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
            ViewData["rivilasku"] = TempData["rivilasku"];
            return View();
        }

        //POST
        [HttpPost]
        public IActionResult Laskuri(Models.Arvopari arvopari)
        {
            ViewData["laskuri"] = arvopari.Laske();
            return View();
        }

        //POST
        [HttpPost]
        public IActionResult Rivilaskuri(string syöte)
        {
            Models.Rivilaskuri rivilaskuri = new();
            Console.WriteLine(syöte);
            rivilaskuri.Jono = syöte;
            TempData["rivilasku"] = rivilaskuri.LaskeMerkkiJono();
            return RedirectToAction("Laskuri", "Demo");
        }
    }
}
