namespace Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Topics", "Ficken");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Topics", "Ficken", c => c.String());
        }
    }
}
