using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using GesCollege.Models;
using GesCollege.PlutoCollege;

namespace GesCollege.Controllers
{
    public class RapportController : Controller
    {
        private CollegeContext db = new CollegeContext();

        public ActionResult Index()
        {
            return View();
        }

        //etat pour les responsables de chauque département
        public ActionResult EtatDepartement(int id)
        {
            List<Departement> allDepatement = new List<Departement>();
            allDepatement = db.Departements.ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/DepartementReport.rpt")));
            rd.SetDataSource(db.Departements.Select(d => new
            {
                idCol = d.College.Id,
                College = d.College.Nom,
                nomDep = d.NomDep,
                nom = d.Directeur.Nom,
                prenom = d.Directeur.Prenom
            }).Where(d => d.idCol == id));
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "DepartementsCollege.pdf");
        }

    //Etat pour la fiche signletique d'un enseignant
    public ActionResult FicheEns(int id)
        {
            Enseignant enseignant = db.Enseignants.Find(id);
            if (enseignant == null)
            {
                return HttpNotFound();
            }
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/FicheEnsReport.rpt")));
            rd.SetDataSource(db.Enseignants.Select(e => new
            {
                libelleCours = e.Course.LibCours,
                nom = e.Nom,
                prenom = e.Prenom,
                mail = e.Mail,
                tel = e.Tel,
                indice = e.Indice,
                nomDep = e.Departement.NomDep,
                datePriseFonction = e.DatePriseFonction,
                numEnseignant  = e.Id
            }).Where(e => e.numEnseignant == id).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "EnseignantFiche.pdf");
        }

        
        //fiche signaletique d'un etudiant
        public ActionResult FicheEtu(int id)
        {
            Etudiant etudiant = db.Etudiants.Find(id);
            if (etudiant == null)
            {
                return HttpNotFound();
            }
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/FicheEtuReport.rpt")));
            rd.SetDataSource(db.Etudiants.Where(e => e.Id == id));
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "EtudiantFiche.pdf");

           
        }

        //Etat course avec les notes et la moyenne de chaque matiere
        public ActionResult EtatCourse(int id)
        {
            List<Enseignant> allCourse = new List<Enseignant>();
            allCourse = db.Enseignants.ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/CourseReport.rpt")));
            rd.SetDataSource(db.Enseignants.Select(c => new
            { Numero = c.CourseId,
                LibelleCours = c.Course.LibCours,
                nom = c.Nom,
                prenom = c.Prenom
            }).Where(e => e.Numero == id));
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Enseignant nommer = new Enseignant();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "RespoCours.pdf");
        }

        //Etat pour les notes par Departements
        public ActionResult EtatEtuDep()
        {
            List<Note> allNote = new List<Note>();
            allNote = db.Notes.ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/EtuDepReport.rpt")));
            rd.SetDataSource(db.Notes.Select(n => new
            {
                LibelleCours = n.Courses.LibCours,
                nom = n.Etudiants.Nom,
                prenom = n.Etudiants.Prenom,
                NoteControle = n.NoteControle 
            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "MoyCourses.pdf");
        }

        //Etat pour les Relevés
        public ActionResult EtatReleve(int id)
        {
            List<Note> allNote = new List<Note>();
            allNote = db.Notes.ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/ReleveReport.rpt")));
            rd.SetDataSource(db.Notes.Select(n => new
            {
                LibelleCours = n.Courses.LibCours,
                nom = n.Etudiants.Nom,
                prenom = n.Etudiants.Prenom,
                NoteControle = n.NoteControle,
                DateEntree = n.Etudiants.DateEntree, 
                NomSalle = n.Courses.Salle.NomSalle,
                NumEtudiant = n.IdEtudiant,
                NumCours = n.IdCourse
            }).Where(e => e.NumEtudiant == id).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Releve.pdf");
        }
        //les matiere sans notes
       /* public ActionResult FicheEtu1(int id)
        {
            Etudiant etudiant = db.Etudiants.Find(id);
            if (etudiant == null)
            {
                return HttpNotFound();
            }
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/FicheEtuReport.rpt")));
            rd.SetDataSource(db.Etudiants.Where(e => e.Id == id));
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "EtudiantFiche.pdf");

        }*/


    }
}
