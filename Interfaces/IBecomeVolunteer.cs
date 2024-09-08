using AsmvBackend.Models;

namespace ServerAsmv.Interfaces {
    public interface IBecomeVolunteer
    {
        List<BecomeVolunteer> GetBecomeVolunteers();
        BecomeVolunteer GetBecomeVolunteer(long Id);
        void AddBecomeVolunteer(BecomeVolunteer newBecomeVolunteer);
        bool UpdateBecomeVolunteer(BecomeVolunteer newBecomeVolunteer);
        void DeleteBecomeVolunteer(long Id);
    }
}
