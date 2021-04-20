using Demo.ReitinhaunLuokat;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Demo.Controllers
{
    [Route("TietokantaAPI")]
    [ApiController]
    public class TietokantaAPIController : Controller
    {

        DemoprojektiContext konteksti = new();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Reseptit")]
        public List<Resepti> HaeReseptit()
        {
            /*Resepti resepti = new Resepti();
            resepti.Aika = 30;
            resepti.Id = 1;
            resepti.Luotu = DateTime.Now;
            resepti.Nimi = "Soppa";
            resepti.Ohje = "Pitkäteksti rotla";
            List<Resepti> reseptilista = new();
            reseptilista.Add(resepti);
            return reseptilista;
            */
            return konteksti.Reseptis.ToList();
        }

        [HttpGet]
        [Route("Resepti/{id}")]
        public Resepti HaeResepti(int id)
        {
            return konteksti.Reseptis.Where(r => r.Id == id).Single();
        }

        [HttpPost]
        [Route("Reseptit/Lisää")]
        public IActionResult LisääResepti(Resepti resepti)
        {
            konteksti.Reseptis.Add(resepti);
            return RedirectToAction("Tietokanta", "Reseptit");
        }

        [HttpGet]
        [Route("Keskustelut")]
        public List<Keskustelu> HaeKeskustelut()
        {
            return konteksti.Keskustelus.ToList();
        }

        [HttpGet]
        [Route("Keskustelu/{id}")]
        public List<Kommentti> HaeKeskustelunViestit(int id)
        {
            IQueryable<Kommentti> dbKommentit = konteksti.Kommenttis.Where(v => v.Keskustelu == id).OrderBy(v => v.Luotu);
            return dbKommentit.ToList();
        }
    }
}
