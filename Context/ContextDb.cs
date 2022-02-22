using Licenta.Models;
using Microsoft.EntityFrameworkCore;
using Task = Licenta.Models.Task;

namespace Licenta.Context
{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions<ContextDb> options) : base(options)
        {

        }

        public DbSet<MemberModel> Members { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Training> Trainings { get; set; }
    }
}
