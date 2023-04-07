using LeaveManagementAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementAPI.Controllers
{
    public class LeaveApplicationController : Controller
    {
        private readonly ILeaveApplicationRepository _leaveApplication;

        public LeaveApplicationController(ILeaveApplicationRepository leaveApplication)
        {
            _leaveApplication = leaveApplication;
        }
    }
}
