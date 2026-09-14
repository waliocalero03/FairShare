using FairShare.API.DTOs;
using FairShare.API.Mappers.Interfaces;
using FairShare.Core;
using Riok.Mapperly.Abstractions;

namespace FairShare.API.Mappers
{
    [Mapper]
    public partial class GroupMapper : IGroupMapper
    {
        // Ignoramos los campos que la base de datos o el constructor de la entidad
        // se encargarán de rellenar por su cuenta.
        [MapperIgnoreTarget(nameof(Group.Id))]
        [MapperIgnoreTarget(nameof(Group.CreatedAt))]
        [MapperIgnoreTarget(nameof(Group.Participants))]
        [MapperIgnoreTarget(nameof(Group.Expenses))]
        public partial Group ToEntity(CreateGroupRequest request);

        // Para la actualización hacemos lo mismo. El 'Id' no viene en el body (DTO), 
        // viene por la URL, así que le decimos a Mapperly que lo ignore.
        [MapperIgnoreTarget(nameof(Group.CreatedAt))]
        [MapperIgnoreTarget(nameof(Group.Participants))]
        [MapperIgnoreTarget(nameof(Group.Expenses))]
        public partial Group ToEntity(UpdateGroupRequest request);
    }
}
