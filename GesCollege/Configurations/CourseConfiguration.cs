using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using GesCollege.Models;

namespace GesCollege.Configurations
{
    public class CourseConfiguration:EntityTypeConfiguration<Course>
    {
        public CourseConfiguration()
        {
            ToTable("Course");
            Property(c => c.Id)
            .IsRequired()
            .HasColumnName("NumCours");

            Property(c => c.LibCours)
            .IsRequired()
            .HasColumnType("Varchar")
            .HasColumnName("LibelleCours")
            .HasMaxLength(30);

            HasMany(c => c.Enseignants)
            .WithRequired(e => e.Course)
            .HasForeignKey(e => e.CourseId);
        }

    }
}