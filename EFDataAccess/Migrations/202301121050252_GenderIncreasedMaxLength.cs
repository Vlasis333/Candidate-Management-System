namespace EFDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenderIncreasedMaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Candidates", "Gender", c => c.String(maxLength: 60));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Candidates", "Gender", c => c.String(maxLength: 1));
        }
    }
}
