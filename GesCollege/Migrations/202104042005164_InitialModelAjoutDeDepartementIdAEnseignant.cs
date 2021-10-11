namespace GesCollege.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModelAjoutDeDepartementIdAEnseignant : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Enseignant", name: "Departement_Id", newName: "DepartementId");
            RenameIndex(table: "dbo.Enseignant", name: "IX_Departement_Id", newName: "IX_DepartementId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Enseignant", name: "IX_DepartementId", newName: "IX_Departement_Id");
            RenameColumn(table: "dbo.Enseignant", name: "DepartementId", newName: "Departement_Id");
        }
    }
}
