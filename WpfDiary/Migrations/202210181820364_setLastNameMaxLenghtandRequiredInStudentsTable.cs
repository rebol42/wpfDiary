namespace WpfDiary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setLastNameMaxLenghtandRequiredInStudentsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "LastName", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "LastName", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
