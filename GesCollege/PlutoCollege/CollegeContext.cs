using System.Data.Entity;
using GesCollege.Models;
using GesCollege.Configurations;


namespace GesCollege.PlutoCollege
{
    public class CollegeContext:DbContext
    {
        public CollegeContext() :base("CollegeContext")
        {
        }
        public virtual DbSet<Etudiant> Etudiants { get; set; }
        public virtual DbSet<Enseignant> Enseignants { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<College> Colleges { get; set; }
        public virtual DbSet<Departement> Departements { get; set; }
        public virtual DbSet<Salle> Salles { get; set; }
         

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CollegeConfiguration());
            modelBuilder.Configurations.Add(new DepartementConfiguration());
            modelBuilder.Configurations.Add(new EtudiantConfiguration());
            modelBuilder.Configurations.Add(new EnseignantConfiguration());
            modelBuilder.Configurations.Add(new SalleConfiguration());
            modelBuilder.Configurations.Add(new CourseConfiguration());
            modelBuilder.Configurations.Add(new NoteConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        //public System.Data.Entity.DbSet<GesCollege.Models.Etudiant> Personnes { get; set; }
    }
}