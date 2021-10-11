using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GesCollege.Models
{
    public class Salle
    {
        public Salle()
        {
            Courses = new HashSet<Course>();
        }
        public int Id { set; get; }
        public string NomSalle { set; get; }
        public  int Capacite { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}