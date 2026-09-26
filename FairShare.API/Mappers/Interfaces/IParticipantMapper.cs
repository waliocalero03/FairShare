using FairShare.API.DTOs.Participant;
using FairShare.Core;

namespace FairShare.API.Mappers.Interfaces
{
    public interface IParticipantMapper
    {
        Participant ToEntity(CreateParticipantRequest request);
        Participant ToEntity(UpdateParticipantRequest request);
    }
}
