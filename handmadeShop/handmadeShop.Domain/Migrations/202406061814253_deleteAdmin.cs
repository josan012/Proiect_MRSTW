namespace handmadeShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteAdmin : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM Orders WHERE Id = 1");

            Sql("DELETE FROM ClientProfiles WHERE Id = '0ff19f6e-97d9-45e5-a168-640a558d4bf1'");

            Sql("DELETE FROM ClientProfiles WHERE Id = '0ff19f6e-97d9-45e5-a168-640a558d4bf1'");

            Sql("DELETE FROM AspNetUsers  WHERE Id = '0ff19f6e-97d9-45e5-a168-640a558d4bf1'");
        }
        
        public override void Down()
        {
        }
    }
}
