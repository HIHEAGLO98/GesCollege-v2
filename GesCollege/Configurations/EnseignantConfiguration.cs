using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using GesCollege.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GesCollege.Configurations
{
    public class EnseignantConfiguration:EntityTypeConfiguration<Enseignant>
    {
        public EnseignantConfiguration()
        {
            ToTable("Enseignant");

            Property(p => p.Id)
            .IsRequired()
            .HasColumnName("NumEnseignant");
            //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); ;

            Property(p => p.Nom)
            .IsRequired()
            .HasColumnType("Varchar")
            .HasMaxLength(30);
            Property(p => p.Prenom)
            .IsRequired()
            .HasColumnType("Varchar")
            .HasMaxLength(30);

            Property(p => p.Mail)
            .IsRequired()
            .HasColumnType("Varchar")
            .HasMaxLength(100);

            Property(e => e.Indice)
            .IsRequired()
            .HasColumnType("Varchar")
            .HasMaxLength(3);

            Property(e => e.DatePriseFonction)
            .IsRequired();


            HasRequired(t => t.Course)
           .WithMany(e => e.Enseignants)
           .HasForeignKey(t => t.CourseId)
           .WillCascadeOnDelete(false);

            //diriger

            HasOptional(e => e.Departement)
           .WithRequired(d => d.Directeur);



            //travailler
            HasRequired(e => e.Departement)
           .WithMany(d => d.Enseignants);











        }
    }
}