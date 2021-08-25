using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using basic_banking_app_server.Models;
using basic_banking_app_server.Data;
using basic_banking_app_server.Dtos;

namespace basic_banking_app_server.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _repository;
        private readonly IMapper _mapper;


        public UsersController(IUserRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            var users = _repository.GetAllUsers();
            var dtoUsers = _mapper.Map<IEnumerable<UserReadDto>>(users);

            return Ok(dtoUsers);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _repository.GetUserById(id);

            if (user == null)
                return NotFound();

            var dtoUser = _mapper.Map<UserReadDto>(user);

            return Ok(dtoUser);
        }

    }
}
