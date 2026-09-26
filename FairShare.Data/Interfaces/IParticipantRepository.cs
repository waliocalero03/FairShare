using FairShare.Core;

namespace FairShare.Data.Interfaces
{
    public interface IParticipantRepository
    {
        Participant? GetParticipantById(int id);
        IEnumerable<Participant> GetParticipantsByGroupId(int groupId);
        bool CreateParticipant(Participant participant);
        bool UpdateParticipant(Participant participant);
        bool DeleteParticipant(int id);
    }
}
