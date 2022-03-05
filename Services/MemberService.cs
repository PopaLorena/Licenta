using Licenta.Models;
using Licenta.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Services
{
    public class MemberService : IMemberService
    {
        private Context.ContextDb _context;
        public MemberService(Context.ContextDb context)
        {
            _context = context;
        }

        public async Task<MemberModel> AddMember(MemberModel member)
        {
            member.StatutChangeDate = member.StartDate;
            _context.Members.Add(member);
            _context.SaveChanges();
            return member;
        }

        public async Task DeleteMember(MemberModel member)
        {
            _context.Members.Remove(member);
            _context.SaveChanges();
        }

        public async Task<MemberModel> EditMember(MemberModel member)
        {
            var existingMember =  _context.Members.Find(member.Id);
            if (existingMember != null)
            {
                existingMember.StatutChangeDate = member.StatutChangeDate;
               
                if (member.Statut != existingMember.Statut && member.StatutChangeDate != existingMember.StatutChangeDate)
                {
                    existingMember.StatutChangeDate = DateTime.Now;
                }

                existingMember.Name = member.Name;
                existingMember.PhotoUrl = member.PhotoUrl;
                existingMember.StartDate = member.StartDate;
                existingMember.Statut = member.Statut;
                existingMember.TelNumber = member.TelNumber;
                existingMember.University = member.University;
                existingMember.BirthDate = member.BirthDate;
                existingMember.Email = member.Email;
              
                _context.Members.Update(existingMember);
                _context.SaveChanges();
            }
            return member;
        }

        public async Task<MemberModel> GetMemberById(int id)
        {
            return  _context.Members.SingleOrDefault(x => x.Id == id);
        }

        public async Task<List<MemberModel>> GetMembers()
        {
            return _context.Members.Include(e => e.Responsibilities).Include(e => e.MemberMeetings).ThenInclude(m => m.Meeting).Include(e => e.MemberTrainings).ToList();
        }
    }
}
