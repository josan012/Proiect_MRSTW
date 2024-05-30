namespace handmadeShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProducts : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Products (Name, Price, Category) VALUES ('Ie', 800,  'HaineNationale')");
            Sql("INSERT INTO Products (Name, Price,Category) VALUES ('Camasa nationala', 750 ,'HaineNationale')");
            Sql("INSERT INTO Products (Name, Price, Category) VALUES ('Catrinta', 700, 'HaineNationale')");
            Sql("INSERT INTO Products (Name, Price, Category) VALUES ('Rochie', 1500,'HaineNationale')");

            // Main Dishes
            Sql("INSERT INTO Products (Name, Price, Category) VALUES ('Ursulet 50 cm', 600, 'Jucarii')");
            Sql("INSERT INTO Products (Name, Price, Category) VALUES ('Iepuras 50 cm', 650, 'Jucarii')");
            Sql("INSERT INTO Products (Name, Price, Category) VALUES ('Ursulet 30 cm', 400, 'Jucarii')");
            Sql("INSERT INTO Products (Name, Price, Category) VALUES ('Iepuras 30 cm', 450, 'Jucarii')");
            Sql("INSERT INTO Products (Name, Price, Category) VALUES ('Iepuras Picioare Lungi', 350, 'Jucarii')");
            Sql("INSERT INTO Products (Name, Price, Category) VALUES ('Lalafanfan', 300, 'Jucarii')");

            // Drinks
            Sql("INSERT INTO Products (Name, Price, Category) VALUES ('Gentuta verde', 900, 'Gentute')");
            Sql("INSERT INTO Products (Name, Price, Category) VALUES ('Gentuta neagra', 900, 'Gentute')");
            Sql("INSERT INTO Products (Name, Price, Category) VALUES ('Gentuta roz', 900, 'Gentute')");
            Sql("INSERT INTO Products (Name, Price, Category) VALUES ('Gentuta alb-negru', 900, 'Drink')");
            Sql("INSERT INTO Products (Name, Price, Category) VALUES ('Gentuta alba', 900, 'Gentute')");
            Sql("INSERT INTO Products (Name, Price, Category) VALUES ('Torbita eco', 400, 'Gentute')");

            // Desserts
            Sql("INSERT INTO Products (Name, Price, Category) VALUES ('Fustita fatin 3-5 ani', 200, 'ArticoleCopii')");
            Sql("INSERT INTO Products (Name, Price, Category) VALUES ('Fustita fatin 6-12 ani', 250, 'ArticoleCopii')");
            Sql("INSERT INTO Products (Name, Price, Category) VALUES ('Papion', 30, 'ArticoleCopii')");
            Sql("INSERT INTO Products (Name, Price, Category) VALUES ('Bentita', 40, 'ArticoleCopii')");
            Sql("INSERT INTO Products (Name, Price, Category) VALUES ('Vesta', 500, 'ArticoleCopii')");
        }
        
        public override void Down()
        {
        }
    }
}
