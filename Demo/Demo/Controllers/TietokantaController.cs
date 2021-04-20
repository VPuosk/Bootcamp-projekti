using Demo.ReitinhaunLuokat;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    [Route("Tietokanta")]
    public class TietokantaController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Reseptit")]
        public IActionResult Reseptit()
        {
            return View();
        }

        [HttpGet]
        [Route("Resepti/{id}")]
        public IActionResult Resepti(int? id)
        {
            // testataan onko oikea
            if (id == null)
            {
                return RedirectToAction("Reseptit","Tietokanta");
            }

            DemoprojektiContext konteksti = new();

            Resepti resepti = konteksti.Reseptis.FirstOrDefault(r => r.Id == id);

            if (resepti == null)
            {
                return RedirectToAction("Reseptit", "Tietokanta");
            }
            return View(resepti);
        }

        [HttpGet]
        [Route("Keskustelut")]
        public IActionResult Keskustelut()
        {
            DemoprojektiContext konteksti = new();
            IQueryable<Keskustelu> keskustelut = konteksti.Keskustelus;
            return View(keskustelut.ToList());
        }

        [HttpGet]
        [Route("Keskustelu/{id}")]
        public IActionResult Keskustelut(int? id)
        {
            // testataan onko oikea
            if (id == null)
            {
                return RedirectToAction("Keskustelut", "Tietokanta");
            }

            DemoprojektiContext konteksti = new();

            IQueryable<Kommentti> kommentit = konteksti.Kommenttis.Where(k => k.Keskustelu == id);

            if (kommentit == null)
            {
                return RedirectToAction("Keskustelut", "Tietokanta");
            }
            return View(kommentit.ToList());
        }

        [HttpPost]
        [Route("Reseptit/Lisää")]
        public IActionResult LisääResepti()
        {
            return RedirectToAction("Tietokanta", "Reseptit");
        }
    }
}
