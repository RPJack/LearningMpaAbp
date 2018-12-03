namespace LearningMpaAbp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_MyTask_Entity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssignedPersonId = c.Long(),
                        Title = c.String(nullable: false, maxLength: 256),
                        Description = c.String(nullable: false),
                        State = c.Byte(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.AssignedPersonId)
                .Index(t => t.AssignedPersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MyTasks", "AssignedPersonId", "dbo.AbpUsers");
            DropIndex("dbo.MyTasks", new[] { "AssignedPersonId" });
            DropTable("dbo.MyTasks");
        }
    }
}
