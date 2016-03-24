namespace IdeaRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ideas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Author = c.String(),
                        Text = c.String(),
                        UserId = c.Int(nullable: false),
                        Confirm = c.Boolean(nullable: false),
                        DeletedByUser = c.Boolean(nullable: false),
                        DeletedByAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ideas", "UserId", "dbo.Users");
            DropIndex("dbo.Ideas", new[] { "UserId" });
            DropTable("dbo.Ideas");
        }
    }
}
