﻿using AutoMapper.Configuration.Annotations;
using E_Commerce.API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("Errors/{statusCode}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)] 
    public class ErorrController : ControllerBase
    {
        [HttpGet]
        public IActionResult ShowError(int statusCode)
        {
            return new ObjectResult(new BaseCommonResponse(statusCode));
        }
    }
}
