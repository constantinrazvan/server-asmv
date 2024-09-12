using AsmvBackend.Models;

namespace ServerAsmv.Interfaces {
    public interface IBecomeVolunteer
    {
        List<BecomeVolunteer> GetBecomeVolunteers();
        BecomeVolunteer GetBecomeVolunteer(long Id);
        void AddBecomeVolunteer(BecomeVolunteer newBecomeVolunteer);
        Task<bool> UpdateBecomeVolunteer(BecomeVolunteer newBecomeVolunteer, long Id);
        void DeleteBecomeVolunteer(long Id);
        Task MarkAsRead(long Id);
    }
}
