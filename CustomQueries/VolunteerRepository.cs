using ServerAsmv.Models;

namespace ServerAsmv.CustomQueries {
    public interface VolunteerRepository {
        Task<Volunteer?> SelectPresidentAsync(string department);
        Task<Volunteer?> SelectVicePresidentAsync(string department);
        Task<List<Volunteer?>> SelectByDepartment(string department)
    }
}