using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using GesCollege.Models;

namespace GesCollege.Configurations
{
    public class CollegeConfiguration:EntityTypeConfiguration<College>
    {
        public CollegeConfiguration()
        {
            ToTable("College");
            Property(c => c.Id)
            .IsRequired()
            .HasColumnName("CodeCollege");
            Property(c => c.Nom)
            .IsRequired()
            .HasColumnType("Varchar")
            .HasMaxLength(30);

            Property(c => c.SiteWeb)
            .IsRequired()
            .HasColumnType("Varchar")
            .HasMaxLength(100);

            //Rélation et clé étrangère
            HasMany(c => c.Departements)
            .WithRequired(d =>d.College)
            .HasForeignKey(d => d.CodeCollege);

            
        }
    }
}