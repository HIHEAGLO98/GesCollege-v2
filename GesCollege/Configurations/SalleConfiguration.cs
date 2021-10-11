using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using GesCollege.Models;

namespace GesCollege.Configurations
{
    public class SalleConfiguration:EntityTypeConfiguration<Salle>
    { 
        public SalleConfiguration()
        {
            ToTable("Salle");
            Property(s => s.Id)
            .IsRequired()
            .HasColumnName("NumSalle");
            Property(s => s.NomSalle)
            .IsRequired()
            .HasMaxLength(10);
            Property(s => s.Capacite)
            .IsRequired();
            //contrainte de clé étrangère
            HasMany(s => s.Courses)
            .WithRequired(c => c.Salle)
            .HasForeignKey(c => c.SalleId);
            

        }
    }
}