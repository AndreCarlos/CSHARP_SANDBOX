namespace LGroup.CodeFirst.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnderecoCpfMigration : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TB_CLIENTE", name: "DS_EMAIL", newName: "NR_CPF");
            AddColumn("dbo.TB_CLIENTE", "Endereco", c => c.String());
            AddColumn("dbo.TB_CLIENTE", "CPF", c => c.String());
            AlterColumn("dbo.TB_CLIENTE", "NR_CPF", c => c.String(nullable: false, maxLength: 15, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TB_CLIENTE", "NR_CPF", c => c.String(nullable: false, maxLength: 30, unicode: false));
            DropColumn("dbo.TB_CLIENTE", "CPF");
            DropColumn("dbo.TB_CLIENTE", "Endereco");
            RenameColumn(table: "dbo.TB_CLIENTE", name: "NR_CPF", newName: "DS_EMAIL");
        }
    }
}
