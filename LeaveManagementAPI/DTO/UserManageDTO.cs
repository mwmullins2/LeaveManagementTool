namespace LeaveManagementAPI.DTO
{
    public class UserManageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int TotalLeaveBalance { get; set; }
        public int AvailableLeaveBalance { get; set; }
    }
}
