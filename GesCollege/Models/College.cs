using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GesCollege.Models
{
    public class College
    {
        public College()
        {
            Departements = new HashSet<Departement>();
        }
        public int Id { set; get; }
        public string Nom { set; get; }
        public string SiteWeb { get; set; }
        //public virtual Departement Departement { set; get; }
       public virtual ICollection<Departement> Departements { set; get; }
    }
}