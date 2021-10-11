using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GesCollege.Models
{
    public class Etudiant
    {
        public Etudiant()
        {
            Notes = new HashSet<Note>();
            Courses = new HashSet<Course>();

        }
        public int Id { set; get; }

        public string Nom { set; get; }

        public string Prenom { set; get; }
        public string Tel { set; get; }

        public string Mail { get; set; }

        public DateTime DateEntree { set; get; }
       
        public virtual ICollection<Note> Notes { set; get; }
        public virtual ICollection<Course> Courses { set; get; }

        public double CalculerMoyenne()
        {

            double somNote = 0;
            foreach (Note ENotes in Notes)
            {
                somNote += ENotes.NoteControle;
            }

            return somNote / Notes.Count();
        }

        public String AfficherMatSansNote() 
        {
            string ListCourse = "";
            int test = 0;
            foreach (Course Mat in Courses)
            {
                foreach (Note ENote in Notes)
                {
                    if (Mat.Id != ENote.IdCourse)
                    {

                    }
                    else { test = 1; }
                }

                if (test == 0)
                {
                    ListCourse = string.Join(",", Mat.LibCours);
                }


            }
            return ListCourse;


        }
        public String AfficherFiche()
        {
            return "Nom:" + this.Nom + " " + this.Prenom + "\nMail:" + this.Mail + "\nTéléphone: " + this.Tel + "";
        }

    }
}