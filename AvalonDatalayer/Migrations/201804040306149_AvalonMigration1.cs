namespace AvalonDatalayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AvalonMigration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Settings_gameAccessLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        GameId = c.Int(),
                    })
                .PrimaryKey(t => t.Username)
                .ForeignKey("dbo.Games", t => t.GameId)
                .Index(t => t.GameId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "GameId", "dbo.Games");
            DropIndex("dbo.Players", new[] { "GameId" });
            DropTable("dbo.Players");
            DropTable("dbo.Games");
        }
    }
}
