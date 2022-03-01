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
          
            modelBuilder.Entity<MemberMeeting>()
                .HasOne(bc => bc.Member)
                .WithMany(b => b.MemberMeetings)
                .HasForeignKey(bc => bc.MemberId);
          
            modelBuilder.Entity<MemberMeeting>()
                .HasOne(bc => bc.Meeting)
                .WithMany(c => c.Participants)
                .HasForeignKey(bc => bc.MeetingId);

    /*        modelBuilder.Entity<MemberModel>()
                 .HasMany<Responsibility>(s => s.Responsibilities)
                 .WithOne(g => g.Responsible)
                 .HasForeignKey(s => s.ResponsibleId);

            modelBuilder.Entity<Event>()
              .HasMany<Responsibility>(s => s.Responsibilities)
              .WithOne(g => g.Event)
              .HasForeignKey(s => s.EventId);
    */
            modelBuilder.Entity<MemberTraining>()
                .HasKey(bc => new { bc.MemberId, bc.TrainingId });

            modelBuilder.Entity<MemberTraining>()
                .HasOne(bc => bc.Member)
                .WithMany(b => b.MemberTrainings)
                .HasForeignKey(bc => bc.MemberId);

            modelBuilder.Entity<MemberTraining>()
                .HasOne(bc => bc.Training)
                .WithMany(c => c.Participants)
                .HasForeignKey(bc => bc.TrainingId);
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
