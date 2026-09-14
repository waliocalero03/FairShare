using FairShare.API.DTOs;
using FairShare.Core;

namespace FairShare.API.Mappers.Interfaces
{
    public interface IGroupMapper
    {
        Group ToEntity(CreateGroupRequest request);
        Group ToEntity(UpdateGroupRequest request);
    }
}
