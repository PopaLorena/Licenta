using Licenta.Models;
using System;
using System.Collections.Generic;

namespace Licenta.Repository
{ 
    public interface IMemberService
    {
        List<MemberModel> GetMembers();

        MemberModel GetMemberById(Guid id);

        MemberModel AddMember(MemberModel member);

        void DeleteMember(MemberModel member);

        MemberModel EditMember(MemberModel member);
    }
}
