using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjekatRVA.Commands.Interfaces;
using ProjekatRVA.Commands.Providers;
using ProjekatRVA.Enums;
using ProjekatRVA.Invokers;
using ProjekatRVA.Logger;
using ProjekatRVA.Models;
using ProjekatRVA.Models.Dto;
using ProjekatRVA.Models.Dto.UserDto;
using ProjekatRVA.Receivers;
using ProjekatRVA.Service.IServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatRVA.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger _logger;
        private IInvoker _invoker;
        private ICommand _command;
        private IReceiver _receiver;
        public AuthenticationController(IInvoker invoker, ICommand command, IReceiver receiver, ILogger logger)
        {
            _invoker = invoker;
            _command = command;
            _receiver = receiver;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                _command = new LoginCommand(_receiver, dto);
                _invoker.AddAndExecute(_command);
 
                AuthenticationDto authenticationDto = await _receiver.LoggedUser(dto);
                _logger.LogEvent(ELog.INFO, authenticationDto.Username + " : Logging");
                return Ok(authenticationDto);
            }
            catch (Exception e)
            {
                _logger.LogEvent(ELog.ERROR, "Error in logging" + " : " + e.Message);
                ErrorDto errorDTO = new ErrorDto() { Message = e.Message };
                return BadRequest(errorDTO);
                throw;
            }
            
        }


        
    }
}
