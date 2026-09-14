using FairShare.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace FairShare.Data.Interfaces
{
    public interface IGroupRepository
    {
        Group? GetGroupById(int idGroup);
        bool CreateGroup(Group group);
        bool UpdateGroup(Group group);
        bool DeleteGroup(int idGroup);
        Group? GetGroupByCode(string code);
    }
}
