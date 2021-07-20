using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Verification.Attributes;
using Verification.Data.IRepos;
using Verification.Dtos;
using Verification.Models;

namespace Verification.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepo productRepo, IMapper mapper, ILogger<UserController> logger)
        {
            _userRepo = productRepo;
            _mapper = mapper;
            _logger = logger;
        }

        [ApiResultFilter]
        [GenerateVCode(1800)]
        [HttpPost("getVerificationCodeByEmail")]
        public async Task<IActionResult> GetVerificationCodeByEmail([FromBody] Parameters parameters)
        {
            try
            {
                if (await _userRepo.IsExisted(parameters.Email))
                {
                    return BadRequest("The email has been occupied");
                }

                return Ok("The verification code has been sent!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "getVerificationCodeByEmail");
                throw;
            }
        }


        [ApiResultFilter]
        [VerifyVCode]
        [HttpPost("createCustomerByEmail")]
        public async Task<IActionResult> CreateCustomerByEmail(UserCreateDto userCreateDto)
        {
            try
            {
                if (await _userRepo.IsExisted(userCreateDto.Email))
                {
                    return BadRequest("The email has been occupied");
                }
                var user = await _userRepo.Create(_mapper.Map<User>(userCreateDto));
                return Ok(_mapper.Map<UserReadDto>(user));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "createCustomerByEmail");
                throw;
            }
        }
    }

    public class Parameters
    {
        [Required] [EmailAddress] public string Email { get; set; }
    }
}