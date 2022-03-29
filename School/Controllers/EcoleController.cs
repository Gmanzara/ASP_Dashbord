using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace School.Controllers
{
    public class EcoleController : Controller
    {

        private readonly DB_SCHOOLEntities1 db;

        public EcoleController()
        {
            db = new School.DB_SCHOOLEntities1();
        }
        // GET: Ecole
        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(Etablissement ecole)
        {
            try
            {
                if (ModelState.IsValid) {
                    db.Etablissement.Add(ecole);
                    db.SaveChanges();
                    return RedirectToAction("Create");
                }
            }
            catch(DataException ex) {
                ModelState.AddModelError("", "Impossible d'enregistrer les modifications.");
            }
            return View();
        }

        ///ACTION READ

        [HttpGet]
        public ActionResult Read()
        {
            var ListEcole = db.Etablissement.ToList();
            return View(ListEcole);
        }


        public ActionResult Delete()
        {
            return View();
        }

    
        public ActionResult Update(int? id)
        {
            var data = db.Etablissement.Where(x => x.IdEtab == id).SingleOrDefault();
            return View(data);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int? id, Etablissement model)
        {
            var data = db.Etablissement.FirstOrDefault(x => x.IdEtab == id);
            if(data != null)
            {
                data.Nom = model.Nom;
                data.Effectif = model.Effectif;
                data.Ville = model.Ville;
                db.SaveChanges();
                return RedirectToAction("Read");
            }
            else
                return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {

            var data = db.Etablissement.FirstOrDefault(x => x.IdEtab == id);
            if (data != null)
            {
                db.Etablissement.Remove(data);
                db.SaveChanges();
                return RedirectToAction("Read");
            }
            else
                return View();
        }

        public JsonResult Stat() {
            var dt = db.Etablissement.Select(c => new
            {
                c.Effectif,
                c.IdEtab,
                c.Ville,
                c.Nom,
            }).ToList();
            return Json(dt, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Ville()
        {
            var data = db.Etablissement.ToList()
                .GroupBy(x => x.Ville)
                .Select(g => new { ville = g.Key, Effectifs = g.Sum(x => x.Effectif) });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}