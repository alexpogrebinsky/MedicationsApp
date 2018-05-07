namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedNoteVendorFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medications", "Vendor", c => c.String());
            AddColumn("dbo.Medications", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Medications", "Notes");
            DropColumn("dbo.Medications", "Vendor");
        }
    }
}
