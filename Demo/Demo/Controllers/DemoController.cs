using Demo.Models;
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
            return View();
        }

        //GET
        public IActionResult Laskuri()
        {
            //ViewData["laskuri"] = 20;
            ViewData["rivilasku"] = TempData["rivilasku"];
            return View();
        }

        //GET
        public IActionResult Reitinhaku()
        {
            //Session
            string[] tulostettavaTieto = (string[]) TempData["ReitinhakuTulos"];
            // varotaan null pointer ongelmaa
            if (tulostettavaTieto == null)
            {
                tulostettavaTieto = System.Array.Empty<string>();
            }
            ViewData["Tulos"] = tulostettavaTieto;
            return View();
        }

        //POST
        [HttpPost]
        public IActionResult ReitinhaunSyöte(string alku, string loppu, string muoto, string tulostus, string kaaret, string toiminto)
        {
            Reitinhaku reitinhaku = new();
            List<string> tulos = reitinhaku.SuoritaHaku(alku, loppu, muoto, tulostus, kaaret, toiminto);
            TempData["ReitinhakuTulos"] = tulos.ToArray();
            return RedirectToAction("Reitinhaku", "Demo");
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
