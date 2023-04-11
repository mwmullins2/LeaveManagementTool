using LeaveManagementAPI.Models;
using LeaveManagementTool.Models;

namespace LeaveManagementAPI.Interfaces
{
    public interface ILeaveApplicationRepository
    {
        ICollection<LeaveApplication> GetLeaveApplications();
        LeaveApplication GetLeaveApplicationById(int id);
        bool LeaveApplicationExists(int leaveId);
        bool CreateLeaveApplication(LeaveApplication leaveApplication);
        bool UpdateLeaveApplication(LeaveApplication leaveApplication);
        bool DeleteLeaveApplication(LeaveApplication leaveApplication);
        bool Save();
    }
}