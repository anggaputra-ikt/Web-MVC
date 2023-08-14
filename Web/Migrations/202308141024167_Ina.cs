namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ina : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FullName = c.String(),
                        Sex = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        BirthPlace = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Students");
        }
    }
}
