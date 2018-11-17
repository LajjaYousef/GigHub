namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFollowing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Followings",
                c => new
                    {
                        followerId = c.String(nullable: false, maxLength: 128),
                        followeeId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.followerId, t.followeeId })
                .ForeignKey("dbo.AspNetUsers", t => t.followerId)
                .ForeignKey("dbo.AspNetUsers", t => t.followeeId)
                .Index(t => t.followerId)
                .Index(t => t.followeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Followings", "followeeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followings", "followerId", "dbo.AspNetUsers");
            DropIndex("dbo.Followings", new[] { "followeeId" });
            DropIndex("dbo.Followings", new[] { "followerId" });
            DropTable("dbo.Followings");
        }
    }
}
