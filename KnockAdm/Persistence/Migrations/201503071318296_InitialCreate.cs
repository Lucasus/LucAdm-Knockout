using System.Data.Entity.Migrations;

public partial class InitialCreate : DbMigration
{
    public override void Up()
    {
        CreateTable(
            "dbo.User",
            c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserName = c.String(),
                    Email = c.String(),
                    HashedPassword = c.String(),
                })
            .PrimaryKey(t => t.Id);
        
    }
    
    public override void Down()
    {
        DropTable("dbo.User");
    }
}
