namespace SystemaVidanta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Article",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NombreArtículo = c.String(nullable: false),
                        Descripción = c.String(nullable: false),
                        Marca = c.String(nullable: false),
                        Modelo = c.String(nullable: false),
                        FechaEntrada = c.String(nullable: false),
                        FechaSalida = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ResguardoDetalle",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdArticulo = c.Int(nullable: false),
                        IdResguardo = c.Int(nullable: false),
                        Guard_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Guard", t => t.Guard_ID)
                .Index(t => t.Guard_ID);
            
            CreateTable(
                "dbo.Guard",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NumColaborador = c.String(),
                        Empresa = c.String(),
                        FolioResguardo = c.String(),
                        FechaResguardo = c.String(),
                        FechaDevolucion = c.String(),
                        TipoMovimiento = c.String(),
                        TipoPrestamo = c.String(),
                        Ubicacion = c.String(),
                        ObservacionesResguardo = c.String(),
                        VoBo = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
            DropForeignKey("dbo.ResguardoDetalle", "Guard_ID", "dbo.Guard");
            DropIndex("dbo.UserRolesMapping", new[] { "RoleId" });
            DropIndex("dbo.UserRolesMapping", new[] { "UserId" });
            DropIndex("dbo.ResguardoDetalle", new[] { "Guard_ID" });
            DropTable("dbo.User");
            DropTable("dbo.UserRolesMapping");
            DropTable("dbo.Role");
            DropTable("dbo.Guard");
            DropTable("dbo.ResguardoDetalle");
            DropTable("dbo.Article");
        }
    }
}
