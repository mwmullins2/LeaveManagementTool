using LeaveManagementAPI.Data;
using LeaveManagementAPI.Interfaces;
using LeaveManagementTool.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementAPI.Repository
{
    public class LeaveApplicationRepository: ILeaveApplicationRepository
    {
        private readonly DataContext _context;

        public LeaveApplicationRepository(DataContext context)
        {
            _context = context;
        }

        //implemented
        public ICollection<LeaveApplication> GetLeaveApplications()
        {
            return _context.LeaveApplications.ToList();
        }

        //implemented
        public LeaveApplication GetLeaveApplicationById(int leaveId)
        {
            return _context.LeaveApplications.Where(l => l.Id == leaveId).FirstOrDefault();
        }

        public bool LeaveApplicationExists(int leaveId)
        {
            return _context.LeaveApplications.Any(l => l.Id == leaveId);
        }

        //implemented
        public bool CreateLeaveApplication(LeaveApplication leaveApplication)
        {
            _context.Add(leaveApplication);
            return Save();
        }

        //implemented
        public bool UpdateLeaveApplication(LeaveApplication leaveApplication)
        {
            _context.Update(leaveApplication);
            return Save();
        }

        //implemented
        public bool DeleteLeaveApplication(LeaveApplication leaveApplication)
        {
            _context.Remove(leaveApplication);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
