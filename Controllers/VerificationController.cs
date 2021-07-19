using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Verification.Data.IRepos;
using Verification.Dtos;
using Verification.Filters;
using Verification.Models;

namespace Verification.Controllers
{
    [ApiController]
    [Route("api")]
    public class VerificationController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public VerificationController(IUserRepo productRepo, IMapper mapper)
        {
            _userRepo = productRepo;
            _mapper = mapper;
        }

        [ServiceFilter(typeof(CustomExceptionFilter))]
        [HttpPost("getVerificationCodeByEmail")]
        public async Task<IActionResult> GetVerificationCodeByEmail(UserCreateDto userCreateDto)
        {
            return Ok(await _userRepo.Create(_mapper.Map<User>(userCreateDto)));
        }
    }
}