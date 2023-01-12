namespace EFDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FKCandidatePhotoId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CandidatePhotoIdentifications", "PhotoIdentificationType_PhotoIdentificationTypeId", "dbo.PhotoIdentificationTypes");
            DropIndex("dbo.CandidatePhotoIdentifications", new[] { "PhotoIdentificationType_PhotoIdentificationTypeId" });
            RenameColumn(table: "dbo.CandidatePhotoIdentifications", name: "PhotoIdentificationType_PhotoIdentificationTypeId", newName: "PhotoIdentificationId");
            AlterColumn("dbo.CandidatePhotoIdentifications", "PhotoIdentificationId", c => c.Int(nullable: false));
            CreateIndex("dbo.CandidatePhotoIdentifications", "PhotoIdentificationId");
            AddForeignKey("dbo.CandidatePhotoIdentifications", "PhotoIdentificationId", "dbo.PhotoIdentificationTypes", "PhotoIdentificationTypeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CandidatePhotoIdentifications", "PhotoIdentificationId", "dbo.PhotoIdentificationTypes");
            DropIndex("dbo.CandidatePhotoIdentifications", new[] { "PhotoIdentificationId" });
            AlterColumn("dbo.CandidatePhotoIdentifications", "PhotoIdentificationId", c => c.Int());
            RenameColumn(table: "dbo.CandidatePhotoIdentifications", name: "PhotoIdentificationId", newName: "PhotoIdentificationType_PhotoIdentificationTypeId");
            CreateIndex("dbo.CandidatePhotoIdentifications", "PhotoIdentificationType_PhotoIdentificationTypeId");
            AddForeignKey("dbo.CandidatePhotoIdentifications", "PhotoIdentificationType_PhotoIdentificationTypeId", "dbo.PhotoIdentificationTypes", "PhotoIdentificationTypeId");
        }
    }
}
