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
            member.User = _context.Users.Find(member.UserId);
            _context.Members.Add(member);
            _context.SaveChanges();
            return member;
        }

        public async Task DeleteMember(MemberModel member)
        {
            _context.Members.Remove(member);
             var user = _context.Users.SingleOrDefault(x => x.Id == member.UserId);
            _context.Users.Remove(user);
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
                existingMember.StartDate = member.StartDate;
                existingMember.Statut = member.Statut;
                existingMember.TelNumber = member.TelNumber;
                existingMember.University = member.University;
                existingMember.BirthDate = member.BirthDate;
                existingMember.Email = member.Email;
                existingMember.University = member.University;

                _context.Members.Update(existingMember);
                _context.SaveChanges();
            }
            return member;
        }

        public async Task<MemberModel> GetMemberById(int id)
        {
            return  _context.Members.SingleOrDefault(x => x.Id == id);
        }

        public async Task<MemberModel> GetMemberByUsername(string Username)
        {   var id = _context.Users.SingleOrDefault(x => x.Username == Username).Id;
            return _context.Members.SingleOrDefault(x => x.UserId == id);
        }

        public async Task<List<MemberModel>> GetMembers()
        {
            return _context.Members.Include(e => e.MemberMeetings).ThenInclude(m => m.Meeting).Include(e => e.MemberTrainings).ToList();
        }

        public async Task<MemberModel> GetMemberByEmail(string email)
        {
            return _context.Members.SingleOrDefault(x => x.Email == email);
        }

        public async Task<MemberModel> GetMemberByTel(string telNr)
        {
            return _context.Members.SingleOrDefault(x => x.TelNumber == telNr);
        }
    }
}
