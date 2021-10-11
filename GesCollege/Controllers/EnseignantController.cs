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
    public class EnseignantController : Controller
    {
        private CollegeContext db = new CollegeContext();

        // GET: Enseignant
        public ActionResult Index(string sortOrder, string currentFilter, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var enseignants = from s in  db.Enseignants.Include(e => e.Course).Include(e => e.Departement)
                              select s;
            if (searchString != null)
            {
                //page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                enseignants = enseignants.Where(s => s.Nom.Contains(searchString)
                                       || s.Prenom.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    enseignants = enseignants.OrderByDescending(s => s.Nom);
                    break;
                case "Date":
                    enseignants = enseignants.OrderBy(s => s.DatePriseFonction);
                    break;
                case "date_desc":
                    enseignants = enseignants.OrderByDescending(s => s.DatePriseFonction);
                    break;
                default:
                    enseignants = enseignants.OrderBy(s => s.Nom);
                    break;
            }
            /*int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(enseignants.ToPagedList(pageNumber, pageSize));*/
             return View(enseignants.ToList());
        }

        // GET: Enseignant/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enseignant enseignant = db.Enseignants.Find(id);
            if (enseignant == null)
            {
                return HttpNotFound();
            }
            return View(enseignant);
        }

        // GET: Enseignant/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "LibCours");
            ViewBag.DepartementId = new SelectList(db.Departements, "Id", "NomDep");
            return View();
        }

        // POST: Enseignant/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nom,Prenom,Tel,Mail,DatePriseFonction,Indice,CourseId,DepartementId")] Enseignant enseignant)
        {
            if (ModelState.IsValid)
            {
                db.Enseignants.Add(enseignant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "LibCours", enseignant.CourseId);
            ViewBag.DepartementId = new SelectList(db.Departements, "Id", "NomDep", enseignant.DepartementId);
            return View(enseignant);
        }

        // GET: Enseignant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enseignant enseignant = db.Enseignants.Find(id);
            if (enseignant == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "LibCours", enseignant.CourseId);
            ViewBag.DepartementId = new SelectList(db.Departements, "Id", "NomDep", enseignant.DepartementId);
            return View(enseignant);
        }

        // POST: Enseignant/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom,Prenom,Tel,Mail,DatePriseFonction,Indice,CourseId,DepartementId")] Enseignant enseignant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enseignant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "LibCours", enseignant.CourseId);
            ViewBag.DepartementId = new SelectList(db.Departements, "Id", "NomDep", enseignant.DepartementId);
            return View(enseignant);
        }

        // GET: Enseignant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enseignant enseignant = db.Enseignants.Find(id);
            if (enseignant == null)
            {
                return HttpNotFound();
            }
            return View(enseignant);
        }

        // POST: Enseignant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enseignant enseignant = db.Enseignants.Find(id);
            db.Enseignants.Remove(enseignant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ExportProfessor()
        {
            List<Enseignant> allProfessor = new List<Enseignant>();
            allProfessor = db.Enseignants.ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/ProfessorReport.rpt")));
            rd.SetDataSource(allProfessor);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "EnseignantList.pdf");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
