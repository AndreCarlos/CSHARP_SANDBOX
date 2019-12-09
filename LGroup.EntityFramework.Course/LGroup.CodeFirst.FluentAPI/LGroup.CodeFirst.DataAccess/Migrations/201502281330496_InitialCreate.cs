namespace LGroup.CodeFirst.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TB_CLIENTE",
                c => new
                    {
                        ID_CLIENTE = c.Int(nullable: false, identity: true),
                        NM_CLIENTE = c.String(nullable: false, maxLength: 35, unicode: false),
                        DS_EMAIL = c.String(nullable: false, maxLength: 30, unicode: false),
                        DT_NASCIMENTO = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.ID_CLIENTE);
            
            CreateTable(
                "dbo.TB_ITEM_PEDIDO",
                c => new
                    {
                        ID_ITEM_PEDIDO = c.Int(nullable: false, identity: true),
                        ID_PEDIDO = c.Int(nullable: false),
                        ID_PRODUTO = c.Int(nullable: false),
                        NR_QUANTIDADE = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID_ITEM_PEDIDO)
                .ForeignKey("dbo.TB_PEDIDO", t => t.ID_PEDIDO, cascadeDelete: true)
                .ForeignKey("dbo.TB_PRODUTO", t => t.ID_PRODUTO, cascadeDelete: true)
                .Index(t => t.ID_PEDIDO)
                .Index(t => t.ID_PRODUTO);
            
            CreateTable(
                "dbo.TB_PEDIDO",
                c => new
                    {
                        ID_PEDIDO = c.Int(nullable: false, identity: true),
                        DT_CADASTRO = c.DateTime(nullable: false),
                        ID_CLIENTE = c.Int(nullable: false),
                        VL_TOTAL = c.Decimal(nullable: false, precision: 8, scale: 2),
                    })
                .PrimaryKey(t => t.ID_PEDIDO)
                .ForeignKey("dbo.TB_CLIENTE", t => t.ID_CLIENTE, cascadeDelete: true)
                .Index(t => t.ID_CLIENTE);
            
            CreateTable(
                "dbo.TB_PRODUTO",
                c => new
                    {
                        ID_PRODUTO = c.Int(nullable: false, identity: true),
                        NM_PRODUTO = c.String(nullable: false, maxLength: 40, unicode: false),
                        NM_DESCRICAO = c.String(nullable: false, maxLength: 200, unicode: false),
                        VL_PRODUTO = c.Decimal(nullable: false, precision: 6, scale: 2),
                        FLG_STATUS = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID_PRODUTO);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TB_ITEM_PEDIDO", "ID_PRODUTO", "dbo.TB_PRODUTO");
            DropForeignKey("dbo.TB_ITEM_PEDIDO", "ID_PEDIDO", "dbo.TB_PEDIDO");
            DropForeignKey("dbo.TB_PEDIDO", "ID_CLIENTE", "dbo.TB_CLIENTE");
            DropIndex("dbo.TB_PEDIDO", new[] { "ID_CLIENTE" });
            DropIndex("dbo.TB_ITEM_PEDIDO", new[] { "ID_PRODUTO" });
            DropIndex("dbo.TB_ITEM_PEDIDO", new[] { "ID_PEDIDO" });
            DropTable("dbo.TB_PRODUTO");
            DropTable("dbo.TB_PEDIDO");
            DropTable("dbo.TB_ITEM_PEDIDO");
            DropTable("dbo.TB_CLIENTE");
        }
    }
}
