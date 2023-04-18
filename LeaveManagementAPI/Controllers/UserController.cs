using AutoMapper;
using LeaveManagementAPI.DTO;
using LeaveManagementAPI.Interfaces;
using LeaveManagementTool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace LeaveManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // HTTP GET endpoint to retrieve all users from the database and return them in the response body
        // Returns a 200 OK status code and an IEnumerable of UserDTOs if successful,
        // otherwise returns a 400 Bad Request status code if the ModelState is invalid.
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDTO>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        // HTTP GET request to get all leave applications of a user by their user Id
        // If user does not exist, returns 404 Not Found status code
        // Uses IUserRepository to get the user's leave applications from the database
        // Uses AutoMapper to map the leave applications to LeaveApplicationDTOs for presentation
        // Returns 200 OK status code and the list of LeaveApplicationDTOs as response body
        [HttpGet("{userId}/leaveApplication")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetLeaveOfAUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }

            var user = _mapper.Map<List<LeaveApplicationDTO>>(_userRepository.GetLeaveOfAUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        // Retrieves a specific user by their ID.
        // Returns a 200 status code and the user object if found, otherwise returns a 404 if the user is not found.
        // Also returns a 400 if the ModelState is not valid.
        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUserById(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var user = _mapper.Map<UserDTO>(_userRepository.GetUserById(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        // This endpoint creates a new user.
        // It checks if the user already exists and returns a 422 status if so.
        // Uses the UserManageDTO instead of UserDTO so all properties of the user can be created.
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserManageDTO userCreate)
        {
            if (userCreate == null)
                return BadRequest(ModelState);

            var users = _userRepository.GetUsers()
                .Where(u => u.Name.Trim().ToUpper() == userCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (users != null)
            {
                ModelState.AddModelError("", "User Already Exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(userCreate);

            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Create");
        }


        // This endpoint updates an existing user using the User ID.
        // Checks if user exists.
        // Uses UserManageDTO instead of UserDTO to allow editing of all user properties.
        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody] UserManageDTO updatedUser)
        {
            if(updatedUser == null)
                return BadRequest(ModelState);

            if(userId != updatedUser.Id) 
                return BadRequest(ModelState);

            if (!_userRepository.UserExists(userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(updatedUser);

            if (!_userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }


        // Deletes a user using the user ID.
        [HttpDelete("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            if(!_userRepository.UserExists(userId))
                return BadRequest(ModelState);

            var userToDelete = _userRepository.GetUserById(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
            }

            return Ok("Successfully Deleted");
        }

        

    }
}
