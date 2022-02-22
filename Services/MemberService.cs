using Licenta.Models;
using Licenta.Repository;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Licenta.Services
{
    public class MemberService : IMemberService
    {
        private Context.ContextDb _context;
        public MemberService(Context.ContextDb context)
        {
            _context = context;
        }
        public MemberModel AddMember(MemberModel member)
        {
            member.Id = Guid.NewGuid();
            _context.Members.Add(member);
            _context.SaveChanges();
            return member;
        }

        public void DeleteMember(MemberModel member)
        {
            _context.Members.Remove(member);
            _context.SaveChanges();
        }

        public MemberModel EditMember(MemberModel member)
        {
            var existingMember = _context.Members.Find(member.Id);
            if (existingMember != null)
            {
                existingMember.Name = member.Name;
                existingMember.PhotoUrl = member.PhotoUrl;
                existingMember.StartDate = member.StartDate;
                existingMember.Statut = member.Statut;
                existingMember.StatutChangeDate = member.StatutChangeDate;
                existingMember.Tasks = member.Tasks;
                existingMember.TelNumber = member.TelNumber;
                existingMember.University = member.University;
                existingMember.BirthDate = member.BirthDate;
                existingMember.Email = member.Email;
                existingMember.Trainings = member.Trainings;
                existingMember.Meetings = member.Meetings;

                _context.Members.Update(existingMember);
                _context.SaveChanges();
            }
            return member;
        }

        public MemberModel GetMemberById(Guid id)
        {
            return _context.Members.SingleOrDefault(x => x.Id == id);
        }

        public List<MemberModel> GetMembers()
        {
            return _context.Members.ToList();
        }
    }
}
