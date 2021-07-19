using System;
using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Verification.Filters;

namespace Verification.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VerificationController : ControllerBase
    {
        [ServiceFilter(typeof(CustomExceptionFilter))]
        [HttpGet]
        public IEnumerable Get()
        {
            throw new Exception("我是醬爆，我要爆了!!!");
        }
    }
}