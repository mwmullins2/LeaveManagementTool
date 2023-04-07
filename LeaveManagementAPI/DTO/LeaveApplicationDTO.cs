namespace LeaveManagementAPI.DTO
{
    public class LeaveApplicationDTO
    {
        public int Id { get; set; }
        public DateTime LeaveStart { get; set; }
        public DateTime LeaveEnd { get; set; }
        public string ReasonForLeave { get; set; }
        public string LeaveApplicationStatus { get; set; }
    }
}
