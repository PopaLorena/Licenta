using Licenta.Models;
using Microsoft.EntityFrameworkCore;
using Responsibility = Licenta.Models.Responsibility;

namespace Licenta.Context
{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions<ContextDb> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MemberMeeting>()
            .HasKey(bc => new { bc.MemberId, bc.MeetingId });

            modelBuilder.Entity<MemberModel>()
              .HasMany(bc => bc.MemberMeetings)
               .WithOne(b => b.Member)
               .HasForeignKey(bc => bc.MemberId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Meeting>()
                .HasMany(bc => bc.Participants)
                .WithOne(c => c.Meeting)
                .HasForeignKey(bc => bc.MeetingId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MemberModel>()
                  .HasMany<Responsibility>(s => s.Responsibilities)
                  .WithOne(g => g.Responsible)
                  .HasForeignKey(s => s.ResponsibleId);

            modelBuilder.Entity<Event>()
              .HasMany<Responsibility>(s => s.Responsibilities)
              .WithOne(g => g.Event)
              .HasForeignKey(s => s.EventId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MemberTraining>()
                .HasKey(bc => new { bc.MemberId, bc.TrainingId });

            modelBuilder.Entity<MemberModel>()
                 .HasMany(bc => bc.MemberTrainings)
                 .WithOne(b => b.Member)
                 .HasForeignKey(bc => bc.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Training>()
                .HasMany(bc => bc.Participants)
                .WithOne(c => c.Training)
                .HasForeignKey(bc => bc.TrainingId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<MemberModel> Members { get; set; }
        public DbSet<Responsibility> Responsibilities { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<MemberTraining> MemberTrainings { get; set; }
        public DbSet<MemberMeeting> MemberMeetings { get; set; }
    }
}
