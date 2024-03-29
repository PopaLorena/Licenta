﻿using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.Repository
{ 
    public interface IMemberService
    {
        Task<List<MemberModel>> GetMembers();

        Task<MemberModel> GetMemberById(int id);

        Task<MemberModel> GetMemberByUsername(string Username);

        Task<MemberModel> AddMember(MemberModel member);

        Task DeleteMember(MemberModel member);

        Task<MemberModel> EditMember(MemberModel member);

        Task<MemberModel> GetMemberByEmail(string email);

        Task<MemberModel> GetMemberByTel(string telNr);

    }
}
