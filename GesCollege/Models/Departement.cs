using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GesCollege.Models
{
    public class Departement
    {
        public Departement()
        {
            //Colleges = new HashSet<College>();
            Enseignants = new HashSet<Enseignant>();
        }
        public int Id { set; get; }
        public string NomDep { set; get; }
        public  int CodeCollege { set; get; }
        public virtual College College { get; set; }
        public virtual Enseignant Directeur { set; get; }
       //public int Directeur_Id { set; get; }

        public virtual ICollection<Enseignant> Enseignants { set; get; }
        public void CalculerMoyenne() { }
    }
}