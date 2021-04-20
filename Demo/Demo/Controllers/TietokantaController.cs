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

        [HttpPost]
        [Route("Keskustelut")]
        public IActionResult LisääKeskustelu([Bind("Nimi")] Keskustelu keskustelu)
        {
            DemoprojektiContext konteksti = new();
            int keskusteluidenMäärä = konteksti.Keskustelus.Count();
            keskustelu.Id = keskusteluidenMäärä;
            keskustelu.Luotu = DateTime.Now;
            konteksti.Keskustelus.Add(keskustelu);
            konteksti.SaveChanges();
            return RedirectToAction("Keskustelut", "Tietokanta");
        }

        [HttpGet]
        [Route("Keskustelu/{id}")]
        public IActionResult Keskustelu(int? id)
        {
            // testataan onko oikea
            if (id == null)
            {
                return RedirectToAction("Keskustelut", "Tietokanta");
            }

            DemoprojektiContext konteksti = new();

            IQueryable<Kommentti> kommentit = konteksti.Kommenttis.Where(k => k.Keskustelu == id).OrderBy(v => v.Luotu);

            if (kommentit == null)
            {
                return RedirectToAction("Keskustelut", "Tietokanta");
            }
            ViewData["keskusteluID"] = id.ToString();
            return View(kommentit.ToList());
        }

        [HttpPost]
        [Route("Keskustelu/{id}")]
        public IActionResult LisääKommentti(int id, [Bind("Otsikko", "Tekija", "Teksti")] Kommentti kommentti)
        {
            DemoprojektiContext konteksti = new();
            kommentti.Keskustelu = id;
            int kommenttienMäärä = konteksti.Kommenttis.Count();
            kommentti.Id = kommenttienMäärä;
            kommentti.Luotu = DateTime.Now;
            konteksti.Kommenttis.Add(kommentti);
            konteksti.SaveChanges();
            return RedirectToAction("Keskustelu", "Tietokanta", new { id });
        }

        [HttpPost]
        [Route("Reseptit/Lisää")]
        public IActionResult LisääResepti()
        {
            return RedirectToAction("Tietokanta", "Reseptit");
        }
    }
}
