using AsmvBackend.DTOs;
using AsmvBackend.Models;

namespace ServerAsmv.Interfaces {
    public interface IBecomeVolunteer
    {
        List<BecomeVolunteer> GetBecomeVolunteers();
        BecomeVolunteer GetBecomeVolunteer(long Id);
        void AddBecomeVolunteer(BecomeVolunteerDTO newBecomeVolunteer);
        Task<bool> UpdateBecomeVolunteer(BecomeVolunteerDTO newBecomeVolunteer, long Id);
        void DeleteBecomeVolunteer(long Id);
        Task<string> MarkAsRead(long Id);
    }
}
