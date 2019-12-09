namespace LGroup.CodeFirst.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FornecedorCategoriaMigration : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TB_CLIENTE", name: "NR_CPF", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.TB_CLIENTE", name: "Endereco", newName: "DS_ENDERECO");
            RenameColumn(table: "dbo.TB_CLIENTE", name: "CPF", newName: "NR_CPF");
            RenameColumn(table: "dbo.TB_CLIENTE", name: "__mig_tmp__0", newName: "DS_EMAIL");
            AddColumn("dbo.TB_PRODUTO", "NM_FORNECEDOR", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AddColumn("dbo.TB_PRODUTO", "NM_CATEGORIA", c => c.String(nullable: false, maxLength: 15, unicode: false));
            AlterColumn("dbo.TB_CLIENTE", "DS_EMAIL", c => c.String(nullable: false, maxLength: 30, unicode: false));
            AlterColumn("dbo.TB_CLIENTE", "DS_ENDERECO", c => c.String(nullable: false, maxLength: 300, unicode: false));
            AlterColumn("dbo.TB_CLIENTE", "NR_CPF", c => c.String(nullable: false, maxLength: 15, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TB_CLIENTE", "NR_CPF", c => c.String());
            AlterColumn("dbo.TB_CLIENTE", "DS_ENDERECO", c => c.String());
            AlterColumn("dbo.TB_CLIENTE", "DS_EMAIL", c => c.String(nullable: false, maxLength: 15, unicode: false));
            DropColumn("dbo.TB_PRODUTO", "NM_CATEGORIA");
            DropColumn("dbo.TB_PRODUTO", "NM_FORNECEDOR");
            RenameColumn(table: "dbo.TB_CLIENTE", name: "DS_EMAIL", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.TB_CLIENTE", name: "NR_CPF", newName: "CPF");
            RenameColumn(table: "dbo.TB_CLIENTE", name: "DS_ENDERECO", newName: "Endereco");
            RenameColumn(table: "dbo.TB_CLIENTE", name: "__mig_tmp__0", newName: "NR_CPF");
        }
    }
}
