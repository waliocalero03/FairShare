using FairShare.API.DTOs.Participant;
using FairShare.API.Mappers.Interfaces;
using FairShare.Core;
using Riok.Mapperly.Abstractions;

namespace FairShare.API.Mappers.Classes
{
    [Mapper]
    public partial class ParticipantMapper : IParticipantMapper
    {
        // Ignoramos el Id porque la BD lo generará solo
        [MapperIgnoreTarget(nameof(Participant.Id))]
        public partial Participant ToEntity(CreateParticipantRequest request);

        // Ignoramos Id y GroupId porque no vienen en el request de actualización
        [MapperIgnoreTarget(nameof(Participant.GroupId))]
        public partial Participant ToEntity(UpdateParticipantRequest request);

        public ParticipantResponse ToResponse(Participant entity)
        {
            return new ParticipantResponse
            {
                Id = entity.Id,
                GroupId = entity.GroupId,
                Name = entity.Name
            };
        }
    }
}
