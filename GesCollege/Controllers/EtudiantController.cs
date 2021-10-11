using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GesCollege.Models;
using GesCollege.PlutoCollege;
using PagedList;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace GesCollege.Controllers
{
    public class EtudiantController : Controller
    {
        private CollegeContext db = new CollegeContext();

        // GET: Etudiant
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var etudiants = from s in db.Etudiants
                            select s;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                etudiants = etudiants.Where(s => s.Nom.Contains(searchString)
                                       || s.Prenom.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    etudiants = etudiants.OrderByDescending(s => s.Nom);
                    break;
                case "Date":
                    etudiants = etudiants.OrderBy(s => s.DateEntree);
                    break;
                case "date_desc":
                    etudiants = etudiants.OrderByDescending(s => s.DateEntree);
                    break;
                default:
                    etudiants = etudiants.OrderBy(s => s.Nom);
                    break;
            }
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View(etudiants.ToPagedList(pageNumber, pageSize));

            //return View(db.Etudiants.ToList());
        }

        // GET: Etudiant/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etudiant etudiant = db.Etudiants.Find(id);
            if (etudiant == null)
            {
                return HttpNotFound();
            }
            return View(etudiant);
        }

        // GET: Etudiant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Etudiant/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nom,Prenom,Tel,Mail,DateEntree")] Etudiant etudiant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Etudiants.Add(etudiant);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }  catch (DataException)
            {
                ModelState.AddModelError("", "Impossible d'enregistrer cet étudiant. Essayez encore, et si le problème persiste voir votre administrateur système.");
            }

            return View(etudiant);
        }

        // GET: Etudiant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etudiant etudiant = db.Etudiants.Find(id);
            if (etudiant == null)
            {
                return HttpNotFound();
            }
            return View(etudiant);
        }

        // POST: Etudiant/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom,Prenom,Tel,Mail,DateEntree")] Etudiant etudiant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(etudiant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(etudiant);
        }

        // GET: Etudiant/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Etudiant etudiant = db.Etudiants.Find(id);
            if (etudiant == null)
            {
                return HttpNotFound();
            }
            return View(etudiant);
        }

        // POST: Etudiant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Etudiant etudiant = db.Etudiants.Find(id);
                db.Etudiants.Remove(etudiant);
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Etat pour tous les étudiants
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportStudents()
        {
            List<Etudiant> allStudents = new List<Etudiant>();
            allStudents = db.Etudiants.ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/StudentReport.rpt")));
            rd.SetDataSource(allStudents);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "EtudiantList.pdf");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult EtatEtudiant()
        {
            List<Etudiant> allEtudiant = new List<Etudiant>();
            allEtudiant = db.Etudiants.ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/EtuReport.rpt")));
            rd.SetDataSource(db.Etudiants.Select(d => new
            {
               
            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListDepartement.pdf");
        }
    }
}
