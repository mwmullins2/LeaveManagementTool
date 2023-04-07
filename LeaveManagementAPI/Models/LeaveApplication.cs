using LeaveManagementAPI.Models;

namespace LeaveManagementTool.Models
{
    public class LeaveApplication
    {
        public int Id { get; set; }
        public DateTime LeaveStart { get; set; }
        public DateTime LeaveEnd { get; set; }
        public string ReasonForLeave { get; set; }
        public string LeaveApplicationStatus { get; set; }
        public ICollection<UserLeave> UserLeaves { get; set; }

    }
}
