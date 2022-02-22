
using Licenta.Models;
using Licenta.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Licenta.Mock
{
    public class MockMemberData : IMemberService
    {
        private List<MemberModel> members = new List<MemberModel>()
        {
            new MemberModel()
            {
                Id = Guid.NewGuid(),
                Name = "Marius"
            },
             new MemberModel()
            {
                Id = Guid.NewGuid(),
                Name = "Armin"
            }
        };

        public MemberModel AddMember(MemberModel member)
        {
            member.Id = Guid.NewGuid();
            members.Add(member);
            return member;
        }

        public void DeleteMember(MemberModel member)
        {
            members.Remove(member);
        }

        public MemberModel EditMember(MemberModel member)
        {
            var existingMember = GetMemberById(member.Id);
            existingMember.Name = member.Name;
            return existingMember;
        }

        public MemberModel GetMemberById(Guid id)
        {
            return members.SingleOrDefault(x => x.Id == id);
        }

        public List<MemberModel> GetMembers()
        {
            return members;
        }
    }
}
