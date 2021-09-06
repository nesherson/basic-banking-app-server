using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

using basic_banking_app_server.Models.UserModel;
using basic_banking_app_server.Data.UserRepo;
using basic_banking_app_server.Dtos.UserDto;
using basic_banking_app_server.Dtos.AuthDto;
using System;

namespace basic_banking_app_server.Controllers.Users
{
    [Authorize]
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepo _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepo repository, IMapper mapper, IConfiguration configuration)
        {
            _configuration = configuration;
            _userRepository = repository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("signup", Name = "SignupUser")]
        [HttpPost]
        public ActionResult<UserReadDto> SignupUser(UserRegisterDto registerModel)
        {
            var userModel = _mapper.Map<User>(registerModel);

            try
            {
                _userRepository.CreateUser(userModel);
                _userRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message});
            }

            var userReadDto = _mapper.Map<UserReadDto>(userModel);

            return Ok(userReadDto);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult LoginUser(UserAuthDto authModel)
        {
            var user = _userRepository.Authenticate(authModel.Email, authModel.Password);

            if (user == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            var userTokenModel = _mapper.Map<UserTokenDto>(user);
            userTokenModel.Token = _userRepository.GenerateJwtToken(user);

            return Ok(userTokenModel);
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            var usersReadModel = _mapper.Map<IEnumerable<UserReadDto>>(users);

            return Ok(usersReadModel);
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);

            if (user == null)
                return NotFound();

            var userReadModel = _mapper.Map<UserReadDto>(user);

            return Ok(userReadModel);
        }

        [HttpPost]
        public ActionResult<UserReadDto> CreateUser(UserCreateDto userCreateDto)
        {
            var userModel = _mapper.Map<User>(userCreateDto);
            _userRepository.CreateUser(userModel);
            _userRepository.SaveChanges();

            var userReadDto = _mapper.Map<UserReadDto>(userModel);

            return CreatedAtRoute(nameof(GetUserById), new { id = userReadDto.Id }, userReadDto);
        }

        [HttpPatch("{id}")]
        public ActionResult UpdateUser(int id, JsonPatchDocument<UserUpdateDto> patchDoc)
        {
            var userRepoModel = _userRepository.GetUserById(id);

            if (userRepoModel == null)
                return NotFound();

            var userToPatch = _mapper.Map<UserUpdateDto>(userRepoModel);
            patchDoc.ApplyTo(userToPatch, ModelState);

            if (!TryValidateModel(userToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(userToPatch, userRepoModel);
            _userRepository.SaveChanges();

            return NoContent();
        }
    }
}
