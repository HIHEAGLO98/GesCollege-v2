using GesCollege.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace GesCollege.Configurations
{
    public class EtudiantConfiguration: EntityTypeConfiguration<Etudiant>
    {
        public EtudiantConfiguration()
        {
            //ici les configurations conernant la classe Etudiant
            ToTable("Etudiant");

            Property(p => p.Id)
           .IsRequired()
            .HasColumnName("NumEtudiant");
           
            //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

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

            Property(e => e.DateEntree)
            .IsRequired();

           
        }
    }
}