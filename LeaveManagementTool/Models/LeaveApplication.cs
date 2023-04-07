namespace LeaveManagementTool.Models
{
    public class LeaveApplication
    {
        public int Id { get; set; }
        public DateTime leaveStart { get; set; }
        public DateTime leaveEnd { get; set; }
        public string reasonForLeave { get; set; }
        public string leaveApplicationStatus { get; set; }

    }
}
