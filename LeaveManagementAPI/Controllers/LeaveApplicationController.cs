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

        // API endpoint for creating a leave application
        // Accepts a LeaveApplicationDTO object in the request body
        // Returns 204 if leave application is successfully created
        // Returns 400 if request is invalid
        // Returns 500 if an error occurs while saving to the database
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateLeaveApplication([FromBody] LeaveApplicationDTO leaveApplicationCreate)
        {
            if (leaveApplicationCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var leaveAppMap = _mapper.Map<LeaveApplication>(leaveApplicationCreate);

            if (!_leaveApplicationRepository.CreateLeaveApplication(leaveAppMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created Leave Application");
        }

        //This endpoint updates a leave application with the given ID.
        //It checks if the input is valid, if the leave application exists, and then updates it.
        //If successful, it returns a success message.
        [HttpPut("{leaveApplicationId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateLeaveApplication(int leaveApplicationId,
            [FromBody] LeaveApplicationDTO updatedLeaveApplication)
        {
            if(updatedLeaveApplication == null)
                return BadRequest(ModelState);

            if(leaveApplicationId != updatedLeaveApplication.Id)
                return BadRequest(ModelState);

            if(!_leaveApplicationRepository.LeaveApplicationExists(leaveApplicationId))
                return NoContent();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var leaveAppMap = _mapper.Map<LeaveApplication>(updatedLeaveApplication);

            if (!_leaveApplicationRepository.UpdateLeaveApplication(leaveAppMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        //This code defines a DELETE endpoint for deleting a specific leave application by ID.
        //It checks if the leave application exists and returns an error if it doesn't.
        //It then attempts to delete the leave application and returns a success message if it succeeds.
        [HttpDelete("{leaveApplicationId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteLeaveApplication(int leaveApplicationId)
        {
            if(!_leaveApplicationRepository.LeaveApplicationExists(leaveApplicationId))
                return BadRequest(ModelState);

            var leaveApplicationToDelete = _leaveApplicationRepository.GetLeaveApplicationById(leaveApplicationId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_leaveApplicationRepository.DeleteLeaveApplication(leaveApplicationToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting leave application.");
            }

            return Ok("Successfully Deleted");
        }
    }
}
