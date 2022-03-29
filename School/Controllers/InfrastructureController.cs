using School.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace School.Controllers
{
    public class InfrastructureController : Controller
    {

        private readonly DB_SCHOOLEntities1 db;

        public InfrastructureController()
        {
            db = new School.DB_SCHOOLEntities1();
        }

        // GET: Infrastructure
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Infrastrucutre infra)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Infrastrucutre.Add(infra);
                    db.SaveChanges();
                    return RedirectToAction("Create");
                }
            }
            catch (DataException ex)
            {
                ModelState.AddModelError("", "Impossible d'enregistrer les modifications.");
            }
            return View();
        }

        [HttpGet]
        public ActionResult read()
        {
            var data = db.Infrastrucutre.ToList();
            return View(data);
        }


        
        public ActionResult Update()
        {
            return View();
        }
        public ActionResult Delete()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
          
                var data = db.Infrastrucutre.FirstOrDefault(x => x.IdInfra == id);
                if (data != null)
                {
                    db.Infrastrucutre.Remove(data);
                    db.SaveChanges();
                    return RedirectToAction("Read");
                }
                else
                    return View();
            }


        public ActionResult Visuel()
        {
            ViewData["data"] = db.Infrastrucutre.ToList();
            ViewData["dt"] = db.Etablissement.ToList();
            ViewData["Avoir"] = db.Avoir.ToList();

            return View();
        }


        [HttpPost]
        public ActionResult Visuel(Avoir avoir)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Avoir.Add(avoir);
                    db.SaveChanges();
                    return RedirectToAction("Visuel");
                }
            }
            catch (DataException ex)
            {
                ModelState.AddModelError("", "Impossible d'enregistrer les modifications.");

            }
            return View();

        }

        public ActionResult ReadVisuel()
        {
            var data = db.Avoir.ToList();
            return View(data);
        }
        public JsonResult Stat()
        {
            var data = db.Database.SqlQuery<NombreInfra>("select I.Description, sum(A.Nombre) as totalNombre from Infrastrucutre I inner join  Avoir A on A.IdInfra=I.IdInfra group by I.Description");
                        return Json(data, JsonRequestBehavior.AllowGet);

            }
        }

}