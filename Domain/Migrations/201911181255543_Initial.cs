namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.String(nullable: false),
                        Start = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Plans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hours = c.Int(nullable: false),
                        Group_Id = c.Int(nullable: false),
                        Subject_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id, cascadeDelete: true)
                .Index(t => t.Group_Id)
                .Index(t => t.Subject_Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Teacher_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id)
                .Index(t => t.Teacher_Id);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Group_Id = c.Int(nullable: false),
                        Lesson_Id = c.Int(nullable: false),
                        Subject_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .ForeignKey("dbo.Lessons", t => t.Lesson_Id, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id, cascadeDelete: true)
                .Index(t => t.Group_Id)
                .Index(t => t.Lesson_Id)
                .Index(t => t.Subject_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.Schedules", "Lesson_Id", "dbo.Lessons");
            DropForeignKey("dbo.Schedules", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Plans", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.Subjects", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.Plans", "Group_Id", "dbo.Groups");
            DropIndex("dbo.Schedules", new[] { "Subject_Id" });
            DropIndex("dbo.Schedules", new[] { "Lesson_Id" });
            DropIndex("dbo.Schedules", new[] { "Group_Id" });
            DropIndex("dbo.Subjects", new[] { "Teacher_Id" });
            DropIndex("dbo.Plans", new[] { "Subject_Id" });
            DropIndex("dbo.Plans", new[] { "Group_Id" });
            DropTable("dbo.Schedules");
            DropTable("dbo.Teachers");
            DropTable("dbo.Subjects");
            DropTable("dbo.Plans");
            DropTable("dbo.Lessons");
            DropTable("dbo.Groups");
        }
    }
}
