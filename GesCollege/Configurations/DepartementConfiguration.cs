using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using GesCollege.Models;

namespace GesCollege.Configurations
{
    public class DepartementConfiguration:EntityTypeConfiguration<Departement>
    {
        public DepartementConfiguration()
        {
            ToTable("Departement");
            //Id
            Property(d => d.Id)
            .HasColumnName("CodeDep")
            .IsRequired();
            HasKey(d => d.Id);
            //changement portant sur le nom 
            Property(d => d.NomDep)
            .IsRequired()
            .HasColumnType("Varchar")
            .HasMaxLength(30);       

          /*  HasRequired(t => t.College)
          .WithMany(e => e.Departements)
         .HasForeignKey(e => e.CodeCollege)
          .WillCascadeOnDelete(false);*/

            /*HasRequired(t => t.Directeur)
           .WithMany(en => en.DepartementT)
           .HasForeignKey(t => t.EnseignantId)
           .WillCascadeOnDelete(false);*/






        }
    }
}