using LeaveManagementTool.Models;

namespace LeaveManagementAPI.Models
{
    public class UserLeave
    {
        public int UserId { get; set; }
        public int LeaveId { get; set;}
        public User User { get; set; }
        public LeaveApplication LeaveApplication { get; set; }
    }
}
