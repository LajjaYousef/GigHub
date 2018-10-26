namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyPropertiesToGig : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Gigs", "Genre_id", "dbo.Genres");
            DropIndex("dbo.Gigs", new[] { "Genre_id" });
            RenameColumn(table: "dbo.Gigs", name: "Artist_Id", newName: "ArtistId");
            RenameIndex(table: "dbo.Gigs", name: "IX_Artist_Id", newName: "IX_ArtistId");
            AddColumn("dbo.Gigs", "GenreId", c => c.Byte(nullable: false));
            AlterColumn("dbo.Gigs", "Genre_id", c => c.Int());
            CreateIndex("dbo.Gigs", "Genre_id");
            AddForeignKey("dbo.Gigs", "Genre_id", "dbo.Genres", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Gigs", "Genre_id", "dbo.Genres");
            DropIndex("dbo.Gigs", new[] { "Genre_id" });
            AlterColumn("dbo.Gigs", "Genre_id", c => c.Int(nullable: false));
            DropColumn("dbo.Gigs", "GenreId");
            RenameIndex(table: "dbo.Gigs", name: "IX_ArtistId", newName: "IX_Artist_Id");
            RenameColumn(table: "dbo.Gigs", name: "ArtistId", newName: "Artist_Id");
            CreateIndex("dbo.Gigs", "Genre_id");
            AddForeignKey("dbo.Gigs", "Genre_id", "dbo.Genres", "id", cascadeDelete: true);
        }
    }
}
