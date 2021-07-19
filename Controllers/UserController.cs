using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Verification.Attributes;
using Verification.Data;
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
            if (await _userRepo.IsExisted(parameters.Email))
            {
                return BadRequest("The email has been occupied");
            }

            return Ok("The verification code has been sent!");
        }


        [ApiResultFilter]
        [HttpPost("createCustomerByEmail")]
        public async Task<IActionResult> CreateCustomerByEmail(UserCreateDto userCreateDto)
        {
            // todo: verify userCreateDto.vCode with session if it is correct;
            var vCode = userCreateDto.VCode;
            var email = userCreateDto.Email;

            // To Verify if the email is existed;
            if (await _userRepo.IsExisted(email))
            {
                return BadRequest("The email has been occupied");
            }

            // To check if the email and vCode is correct;
            // if (false)
            // {
            //     return BadRequest("The vCode is invalid");
            // }

            return Ok(await _userRepo.Create(_mapper.Map<User>(userCreateDto)));
        }
    }

    public class Parameters
    {
        [Required] [EmailAddress] public string Email { get; set; }
    }
}