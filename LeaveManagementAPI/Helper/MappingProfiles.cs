using AutoMapper;
using LeaveManagementAPI.DTO;
using LeaveManagementTool.Models;

namespace LeaveManagementAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<LeaveApplicationDTO, LeaveApplication>();
            CreateMap<LeaveApplication, LeaveApplicationDTO>();
            CreateMap<UserManageDTO, User>();
            CreateMap<User, UserManageDTO>();
        }
    }
}
