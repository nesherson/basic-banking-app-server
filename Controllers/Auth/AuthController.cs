using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using basic_banking_app_server.Data.AuthRepo;
using basic_banking_app_server.Data.UserRepo;
using basic_banking_app_server.Dtos.UserDto;
using basic_banking_app_server.Models;
using System.Net;

namespace basic_banking_app_server.Controllers.Auth
{
    [Route("")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo _authRepository = null;
        private readonly IUserRepo _userRepository = null;
        private readonly IMapper _mapper = null;

        public AuthController(IAuthRepo authRepository, IUserRepo userRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [Route("signup", Name = "SignupUser")]
        [HttpPost]
        public ActionResult<UserReadDto> SignupUser(UserCreateDto userCreateDto)
        {
            var userModel = _mapper.Map<User>(userCreateDto);
            bool isEmailUsed = _authRepository.IsEmailUsed(userModel.Email);

            if (isEmailUsed)
                return Conflict(new { message = "Email already exists." });

            string hashedPassword = _authRepository.HashPassword(userModel.Password);

            userModel.Password = hashedPassword;

            _userRepository.CreateUser(userModel);
            _userRepository.SaveChanges();

            var userReadDto = _mapper.Map<UserReadDto>(userModel);

            return CreatedAtRoute(nameof(SignupUser), userReadDto);
        }

    }
}
