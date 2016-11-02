namespace CrossPlatform.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class CreateTable_Produto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Produto",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Nome = c.String(nullable: false, maxLength: 50),
                    Descricao = c.String(),
                    CodigoBarra = c.String(),
                    Ativo = c.Boolean(nullable: false),
                    ValorUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                    DataInclusao = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    DataUltimaAlteracao = c.DateTime(precision: 7, storeType: "datetime2"),
                    UsuarioInclusaoId = c.String(nullable: false, maxLength: 128),
                    UsuarioAlteracaoId = c.String(maxLength: 128),
                    Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UsuarioAlteracaoId)
                .ForeignKey("dbo.AspNetUsers", t => t.UsuarioInclusaoId)
                .Index(t => t.UsuarioInclusaoId)
                .Index(t => t.UsuarioAlteracaoId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Produto", "UsuarioInclusaoId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Produto", "UsuarioAlteracaoId", "dbo.AspNetUsers");
            DropIndex("dbo.Produto", new[] { "UsuarioAlteracaoId" });
            DropIndex("dbo.Produto", new[] { "UsuarioInclusaoId" });
            DropTable("dbo.Produto");
        }
    }
}
