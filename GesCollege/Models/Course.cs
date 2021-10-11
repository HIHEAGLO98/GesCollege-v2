using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GesCollege.Models
{
    public class Course
    {
        public Course()
        {
            //Salles = new HashSet<Salle>();
            Enseignants = new HashSet<Enseignant>();
            Etudiants = new HashSet<Etudiant>();
            Notes = new HashSet<Note>();
        }
        public int Id { set; get; }
        public string LibCours { set; get; }
        public virtual Salle Salle { get; set; }
        public int SalleId { get; set; }
        public virtual ICollection<Enseignant> Enseignants { set; get; }
        public virtual ICollection<Etudiant> Etudiants { set; get; }
        public virtual ICollection<Note> Notes { get;  set; }

        public double CalculerMoyenne() 
        {
            double somNote = 0;
            foreach (Note ENotes in Notes)
            {
                somNote += ENotes.NoteControle;
            }

            return somNote / Notes.Count();

        }
    }
}