using System.ComponentModel.DataAnnotations.Schema;

namespace ServerAsmv.Models
{
    public class ActivityUser
    {
        public long ActivityId { get; set; }
        public Activity? Activity { get; set; }

        public long UserId { get; set; }
        public User? User { get; set; }
    }
}
