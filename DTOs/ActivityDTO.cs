namespace ServerAsmv.DTOs
{
    public class ActivityDTO
    {
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public long CoordinatorId { get; set; } 
        public List<long> AssignedUserIds { get; set; } = new List<long>(); 
    }
}
