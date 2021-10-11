using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GesCollege.Models
{
    public class Note
    {
       
        public int Id { get; set; }
        public double NoteControle { set; get; }
        public virtual Course Courses { get; set; }
        public virtual Etudiant Etudiants { get; set; }
        public int IdCourse { get; set; }
        public int IdEtudiant { get; set; }
      
    }
}