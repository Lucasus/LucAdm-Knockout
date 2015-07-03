using System.Data.Entity.Migrations;

public partial class UserCustomMap : DbMigration
{
    public override void Up()
    {
        RenameTable(name: "dbo.User", newName: "Users");
        AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 25));
        AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
    }
    
    public override void Down()
    {
        AlterColumn("dbo.Users", "Email", c => c.String());
        AlterColumn("dbo.Users", "UserName", c => c.String());
        RenameTable(name: "dbo.Users", newName: "User");
    }
}
