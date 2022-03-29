using School.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace School.Controllers
{
    public class VisuelController : Controller
    {

        private readonly DB_SCHOOLEntities1 db;

        public VisuelController()
        {
            db = new School.DB_SCHOOLEntities1();
        }

        public ActionResult Visuelle()
        {
            return View();
        }


        public ActionResult Visuelle(Avoir avoir)
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

        public JsonResult ChartVisuel()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var etabInfra = db.Database.SqlQuery<Models.InfrastructureStart>("select * from Infrastrucutre i  join ( select etab.IdEtab, etab.Nom, av.IdInfra, av.Nombre from Etablissement etab right join Avoir av on etab.idEtab = av.IdEtab) as t on i.IdInfra = t.IdInfra;");
            var etab = db.Etablissement.ToList();
            var infra = db.Infrastrucutre.ToList();
            return 
            
               Json(new{
                    etabInfra = etabInfra,
                    etab = etab,
                    infra = infra
                }, JsonRequestBehavior.AllowGet);

             }

        }


}