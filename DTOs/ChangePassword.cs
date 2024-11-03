namespace ServerAsmv.DTOs { 
    public class ChangePasswordDTO { 
        public long Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}