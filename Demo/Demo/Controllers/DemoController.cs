using Demo.ReitinhaunLuokat;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.Controllers
{
    [Route("Demo")]
    public class DemoController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        //GET
        [HttpGet]
        [Route("Laskuri")]
        public IActionResult Laskuri()
        {
            //ViewData["laskuri"] = 20;
            ViewData["rivilasku"] = TempData["rivilasku"];
            return View();
        }

        //GET
        [HttpGet]
        [Route("Reitinhaku")]
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

        //GET
        [HttpGet]
        [Route("JavaScript")]
        public IActionResult JavaScript()
        {
            return View();
        }

        //
        //POST
        [HttpPost]
        [Route("Reitinhaku")]
        public IActionResult Reitinhaku(string alku, string loppu, string muoto, string tulostus, string kaaret, string toiminto)
        {
            Reitinhaku reitinhaku = new();
            List<string> tulos = reitinhaku.SuoritaHaku(alku, loppu, muoto, tulostus, kaaret, toiminto);
            TempData["ReitinhakuTulos"] = tulos.ToArray();
            //ViewData["Tulos"] = tulos.ToArray()

            //return View(palautin);

            return RedirectToAction("Reitinhaku", "Demo");
        }

        //POST
        [HttpPost]
        [Route("Laskuri")]
        public IActionResult Laskuri(Arvopari arvopari)
        {
            ViewData["laskuri"] = arvopari.Laske();
            return View();
        }

        //POST
        [HttpPost]
        [Route("Rivilaskuri")]
        public IActionResult Rivilaskuri(string syöte)
        {
            Rivilaskuri rivilaskuri = new();
            Console.WriteLine(syöte);
            rivilaskuri.Jono = syöte;
            TempData["rivilasku"] = rivilaskuri.LaskeMerkkiJono();
            return RedirectToAction("Laskuri", "Demo");
        }
    }
}
