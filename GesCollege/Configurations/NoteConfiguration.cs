using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using GesCollege.Models;

namespace GesCollege.Configurations
{
    public class NoteConfiguration:EntityTypeConfiguration<Note>
    {
        public NoteConfiguration()
        {
            Property(n => n.Id)
            .IsRequired()
            .HasColumnName("NumNote");
            HasRequired(t => t.Courses)
           .WithMany(e => e.Notes)
           .HasForeignKey(e => e.IdCourse)
           .WillCascadeOnDelete(false);

            HasRequired(n => n.Etudiants)
          .WithMany(et => et.Notes)
          .HasForeignKey(e => e.IdEtudiant)
          .WillCascadeOnDelete(false);
            Map(n => n.ToTable("Note"));

           
            
           


        }

        
       
    }
}