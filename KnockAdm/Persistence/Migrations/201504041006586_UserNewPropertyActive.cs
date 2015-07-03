using System.Data.Entity.Migrations;

public partial class UserNewPropertyActive : DbMigration
{
    public override void Up()
    {
        RenameTable(name: "dbo.Users", newName: "User");
        AddColumn("dbo.User", "Active", c => c.Boolean(nullable: false));
    }
    
    public override void Down()
    {
        DropColumn("dbo.User", "Active");
        RenameTable(name: "dbo.User", newName: "Users");
    }
}
