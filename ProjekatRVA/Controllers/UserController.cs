using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjekatRVA.Commands.Interfaces;
using ProjekatRVA.Commands.Providers;
using ProjekatRVA.Enums;
using ProjekatRVA.Invokers;
using ProjekatRVA.Logger;
using ProjekatRVA.Models.Dto;
using ProjekatRVA.Models.Dto.UserDto;
using ProjekatRVA.Receivers;
using ProjekatRVA.Service.IServices;
using System;
using System.Collections.Generic;

namespace ProjekatRVA.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;
        private IInvoker _invoker;
        private ICommand _command;
        private IReceiver _receiver;
        public UserController(IInvoker invoker, ICommand command, IReceiver receiver, ILogger logger)
        {
            _invoker = invoker;
            _command = command;
            _receiver = receiver;
            _logger = logger;
        }


        [HttpPost("getUser")]
        public IActionResult GetUser([FromBody]TokenDto tokenDto) {
            try
            {
                UserDto userDto = _receiver.GetUser(tokenDto);
                return Ok(userDto);
            }
            catch (Exception e)
            {
                ErrorDto errorDTO = new ErrorDto() { Message = e.Message };
                return BadRequest(errorDTO);
                throw;
            }
        }
        [HttpPut("editProfile")]
        public IActionResult EditProfile([FromBody]EditUserDto editUserDto) {
            string username = _receiver.GetUsernameByToken(editUserDto.Token);
            try
            {
                _logger.LogEvent(ELog.INFO, username + " : EditProfile");
                _command = new EditUserCommand(_receiver, editUserDto);
                _invoker.AddAndExecute(_command);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogEvent(ELog.ERROR, username + " : "+e.Message);
                ErrorDto errorDTO = new ErrorDto() { Message = e.Message };
                return BadRequest(errorDTO);
                throw;
            }
        }
        [HttpPost("registerUser")]
        public IActionResult RegisterUser(RegisterDto registerDto) {
            string username = _receiver.GetUsernameByToken(registerDto.Token);
            try
            {
                _logger.LogEvent(ELog.INFO, username + " : RegisterUser");
                _command = new RegisterCommand(_receiver, registerDto);
                _invoker.AddAndExecute(_command);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogEvent(ELog.ERROR, username + " : "+e.Message);
                ErrorDto errorDTO = new ErrorDto() { Message = e.Message };
                return BadRequest(errorDTO);
                throw;
            }
        }
        [HttpPost("logout")]
        public IActionResult Logout([FromBody] TokenDto tokenDto) {
            string username = _receiver.GetUsernameByToken(tokenDto.Token);
            try
            {
                _logger.LogEvent(ELog.INFO, username + " : Logout");
                _command = new LogoutCommand(_receiver, tokenDto);
                _invoker.AddAndExecute(_command);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogEvent(ELog.ERROR, username + " : "+e.Message);
                ErrorDto errorDTO = new ErrorDto() { Message = e.Message };
                return BadRequest(errorDTO);
                throw;
            }
        }

        [HttpPost("getLogs")]
        public IActionResult GetLogs([FromBody]TokenDto tokenDto) {
            try
            {
                List<LogDto> logs = _receiver.GetLogs(tokenDto);
                return Ok(logs);
            }
            catch (Exception e )
            {
                ErrorDto errorDTO = new ErrorDto() { Message = e.Message };
                return BadRequest(errorDTO);
                throw;
            }
        }

    }
}
