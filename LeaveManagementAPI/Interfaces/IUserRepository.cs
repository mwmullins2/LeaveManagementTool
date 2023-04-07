using LeaveManagementAPI.Models;
using LeaveManagementTool.Models;

namespace LeaveManagementAPI.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        ICollection<LeaveApplication> GetLeaveOfAUser(int  userId);
        ICollection<User> GetUserByLeave(int  leaveId);
        User GetUserById(int id);
        bool UserExists(int userId);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();

    }
}
