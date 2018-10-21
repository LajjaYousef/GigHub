namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenresTable : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Genres ON");
            Sql("INSERT INTO Genres (id , Name) VALUES (1,'JAZZ')");
            Sql("INSERT INTO Genres (id , Name) VALUES (2,'Blues')");
            Sql("INSERT INTO Genres (id , Name) VALUES (3,'Rock')");
            Sql("INSERT INTO Genres (id , Name) VALUES (4,'Country')");
            Sql("SET IDENTITY_INSERT Genres OFF");

        }
        
        public override void Down()
        {

            Sql("DELETE FROM Genres WHERE id IN(1, 2, 3, 4)");
        }
    }
}
