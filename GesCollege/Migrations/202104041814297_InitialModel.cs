namespace GesCollege.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.College",
                c => new
                    {
                        CodeCollege = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 30, unicode: false),
                        SiteWeb = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.CodeCollege);
            
            CreateTable(
                "dbo.Departement",
                c => new
                    {
                        CodeDep = c.Int(nullable: false, identity: true),
                        NomDep = c.String(nullable: false, maxLength: 30, unicode: false),
                        CodeCollege = c.Int(nullable: false),
                        Directeur_Id = c.Int(),
                    })
                .PrimaryKey(t => t.CodeDep)
                .ForeignKey("dbo.Enseignant", t => t.Directeur_Id)
                .ForeignKey("dbo.College", t => t.CodeCollege, cascadeDelete: true)
                .Index(t => t.CodeCollege)
                .Index(t => t.Directeur_Id);
            
            CreateTable(
                "dbo.Enseignant",
                c => new
                    {
                        NumEnseignant = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 30, unicode: false),
                        Prenom = c.String(nullable: false, maxLength: 30, unicode: false),
                        Tel = c.String(),
                        Mail = c.String(nullable: false, maxLength: 100, unicode: false),
                        DatePriseFonction = c.DateTime(nullable: false),
                        Indice = c.String(nullable: false, maxLength: 3, unicode: false),
                        CourseId = c.Int(nullable: false),
                        Departement_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NumEnseignant)
                .ForeignKey("dbo.Course", t => t.CourseId)
                .ForeignKey("dbo.Departement", t => t.Departement_Id, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.Departement_Id);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        NumCours = c.Int(nullable: false, identity: true),
                        LibelleCours = c.String(nullable: false, maxLength: 30, unicode: false),
                        SalleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NumCours)
                .ForeignKey("dbo.Salle", t => t.SalleId, cascadeDelete: true)
                .Index(t => t.SalleId);
            
            CreateTable(
                "dbo.Etudiant",
                c => new
                    {
                        NumEtudiant = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 30, unicode: false),
                        Prenom = c.String(nullable: false, maxLength: 30, unicode: false),
                        Tel = c.String(),
                        Mail = c.String(nullable: false, maxLength: 100, unicode: false),
                        DateEntree = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NumEtudiant);
            
            CreateTable(
                "dbo.Note",
                c => new
                    {
                        NumNote = c.Int(nullable: false, identity: true),
                        NoteControle = c.Double(nullable: false),
                        IdCourse = c.Int(nullable: false),
                        IdEtudiant = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NumNote)
                .ForeignKey("dbo.Course", t => t.IdCourse)
                .ForeignKey("dbo.Etudiant", t => t.IdEtudiant)
                .Index(t => t.IdCourse)
                .Index(t => t.IdEtudiant);
            
            CreateTable(
                "dbo.Salle",
                c => new
                    {
                        NumSalle = c.Int(nullable: false, identity: true),
                        NomSalle = c.String(nullable: false, maxLength: 10),
                        Capacite = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NumSalle);
            
            CreateTable(
                "dbo.EtudiantCourses",
                c => new
                    {
                        Etudiant_Id = c.Int(nullable: false),
                        Course_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Etudiant_Id, t.Course_Id })
                .ForeignKey("dbo.Etudiant", t => t.Etudiant_Id, cascadeDelete: true)
                .ForeignKey("dbo.Course", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.Etudiant_Id)
                .Index(t => t.Course_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departement", "CodeCollege", "dbo.College");
            DropForeignKey("dbo.Departement", "Directeur_Id", "dbo.Enseignant");
            DropForeignKey("dbo.Enseignant", "Departement_Id", "dbo.Departement");
            DropForeignKey("dbo.Course", "SalleId", "dbo.Salle");
            DropForeignKey("dbo.Note", "IdEtudiant", "dbo.Etudiant");
            DropForeignKey("dbo.Note", "IdCourse", "dbo.Course");
            DropForeignKey("dbo.EtudiantCourses", "Course_Id", "dbo.Course");
            DropForeignKey("dbo.EtudiantCourses", "Etudiant_Id", "dbo.Etudiant");
            DropForeignKey("dbo.Enseignant", "CourseId", "dbo.Course");
            DropIndex("dbo.EtudiantCourses", new[] { "Course_Id" });
            DropIndex("dbo.EtudiantCourses", new[] { "Etudiant_Id" });
            DropIndex("dbo.Note", new[] { "IdEtudiant" });
            DropIndex("dbo.Note", new[] { "IdCourse" });
            DropIndex("dbo.Course", new[] { "SalleId" });
            DropIndex("dbo.Enseignant", new[] { "Departement_Id" });
            DropIndex("dbo.Enseignant", new[] { "CourseId" });
            DropIndex("dbo.Departement", new[] { "Directeur_Id" });
            DropIndex("dbo.Departement", new[] { "CodeCollege" });
            DropTable("dbo.EtudiantCourses");
            DropTable("dbo.Salle");
            DropTable("dbo.Note");
            DropTable("dbo.Etudiant");
            DropTable("dbo.Course");
            DropTable("dbo.Enseignant");
            DropTable("dbo.Departement");
            DropTable("dbo.College");
        }
    }
}
