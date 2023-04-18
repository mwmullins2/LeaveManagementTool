using AutoMapper;
using LeaveManagementAPI.DTO;
using LeaveManagementAPI.Interfaces;
using LeaveManagementAPI.Repository;
using LeaveManagementTool.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LeaveApplicationController : Controller
    {
        private readonly ILeaveApplicationRepository _leaveApplicationRepository;
        private readonly IMapper _mapper;

        public LeaveApplicationController(ILeaveApplicationRepository leaveApplicationRepository, IMapper mapper)
        {
            _leaveApplicationRepository = leaveApplicationRepository;
            _mapper = mapper;
        }

        // This API endpoint returns all leave applications in the database as an IEnumerable of LeaveApplication objects in JSON format.
        // The method uses AutoMapper to map the returned entities to DTOs to hide sensitive data.
        // If the ModelState is not valid, a Bad Request status is returned.
        // If successful, a 200 OK status is returned along with the collection of leave applications.
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LeaveApplication>))]
        public IActionResult GetLeaveApplications()
        {
            var leaveApplications = _mapper.Map<List<LeaveApplicationDTO>>(_leaveApplicationRepository.GetLeaveApplications());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(leaveApplications);
        }

        // This endpoint retrieves a specific leave application by its ID.
        // If the leave application does not exist, a 404 status is returned.
        // If the request is invalid, a 400 status is returned.
        [HttpGet("{leaveApplicationId}")]
        [ProducesResponseType(200, Type = typeof(LeaveApplication))]
        [ProducesResponseType(400)]
        public IActionResult GetLeaveApplicationById(int leaveApplicationId)
        {
            if (!_leaveApplicationRepository.LeaveApplicationExists(leaveApplicationId))
                return NotFound();

            var leaveApplication = _mapper.Map<LeaveApplicationDTO>(_leaveApplicationRepository.GetLeaveApplicationById(leaveApplicationId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(leaveApplication);
        }

    }
}
