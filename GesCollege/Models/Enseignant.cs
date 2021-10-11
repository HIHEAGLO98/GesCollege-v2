using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GesCollege.Models
{
    public class Enseignant
    {
        public Enseignant()
        {
            //Departements = new HashSet<Departement>();
           // Courses = new HashSet<Course>();
        }
        public int Id { set; get; }

        public string Nom { set; get; }

        public string Prenom { set; get; }
        public string Tel { set; get; }

        public string Mail { get; set; }

        public  DateTime DatePriseFonction { set; get; }
        
        public string Indice { get; set; }
        public virtual Departement Departement { get; set; }
       // public virtual Departement DepartementT { get; set; }
        public virtual Course Course { get; set; }
        public int CourseId { get; set; }
        public int DepartementId { get; set; }
        public String AfficherFiche()
        {
            return "Nom:" + this.Nom + " " + this.Prenom + "\nMail:" + this.Mail + "\nTéléphone: " + this.Tel + "";
        }
    }
}