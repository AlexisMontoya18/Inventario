namespace SystemaVidanta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRolesMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        Level = c.Int(nullable: false),
                        Active = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRolesMapping", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRolesMapping", "RoleId", "dbo.Role");
            DropIndex("dbo.UserRolesMapping", new[] { "RoleId" });
            DropIndex("dbo.UserRolesMapping", new[] { "UserId" });
            DropTable("dbo.User");
            DropTable("dbo.UserRolesMapping");
            DropTable("dbo.Role");
        }
    }
}
