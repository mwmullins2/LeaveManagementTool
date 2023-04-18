using LeaveManagementAPI.Data;
using LeaveManagementAPI.Interfaces;
using LeaveManagementTool.Models;

namespace LeaveManagementAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        //Implemented
        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.Id).ToList();
        }
        //Implemented
        public ICollection<LeaveApplication> GetLeaveOfAUser(int userId)
        {
          return _context.UserLeaves.Where(l => l.User.Id == userId)
              .Select(l => l.LeaveApplication).ToList();
        }
        
        public ICollection<User> GetUserByLeave(int leaveId)
        {
            return _context.UserLeaves.Where(l => l.LeaveApplication.Id == leaveId).Select(l => l.User).ToList();
        }
        //Implemented
        public User GetUserById(int id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }

        //Implemented
        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        //Implemented
        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        //Implemented
        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
