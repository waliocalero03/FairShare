using FairShare.API.DTOs.Expense;
using FairShare.API.DTOs.ExpenseSplit;
using FairShare.API.DTOs.Group;
using FairShare.API.DTOs.Participant;
using FairShare.Core;

namespace FairShare.API.Mappers.Interfaces
{
    public interface IGroupMapper
    {
        Group ToEntity(CreateGroupRequest request);
        Group ToEntity(UpdateGroupRequest request);
        GroupResponse ToResponse(Group entity);
        public GroupDetailDTO ToDetailDTO(Group entity, List<ParticipantResponse> participants, List<ExpenseDetailDTO> expenses, List<ExpenseSplitDTO> expenseSplits);
    }
}
