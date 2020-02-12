using Domain.Models;
using System.Data.Entity;

namespace Domain
{
    public class AcademyContext : DbContext
    {
        public AcademyContext() : base("data source=VLAD-PC\\SQLEXPRESS;Initial Catalog=BestAcademy;Integrated Security=True;")
        {

        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Plan> PlanRecords { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Schedule> ScheduleRecords { get; set; }
    }
}