using LeaveManagementAPI.Models;
using LeaveManagementTool.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<LeaveApplication> LeaveApplications { get; set; }
        public DbSet<UserLeave> UserLeaves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLeave>()
                .HasKey(ul => new { ul.UserId, ul.LeaveId });
            modelBuilder.Entity<UserLeave>()
                .HasOne(u => u.User)
                .WithMany(ul => ul.UserLeaves)
                .HasForeignKey(u => u.UserId);
            modelBuilder.Entity<UserLeave>()
                .HasOne(l => l.LeaveApplication)
                .WithMany(ul => ul.UserLeaves)
                .HasForeignKey(l => l.LeaveId);
        }
    }
}
