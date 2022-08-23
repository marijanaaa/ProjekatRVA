using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjekatRVA.Commands.Interfaces;
using ProjekatRVA.Commands.Providers;
using ProjekatRVA.Enums;
using ProjekatRVA.Invokers;
using ProjekatRVA.Logger;
using ProjekatRVA.Models;
using ProjekatRVA.Models.Dto;
using ProjekatRVA.Models.Dto.EventDto;
using ProjekatRVA.Receivers;
using ProjekatRVA.Service.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjekatRVA.Controllers
{
    [Route("api/event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ILogger _logger;
        private IInvoker _invoker;
        private ICommand _command;
        private IReceiver _receiver;
        public EventController(IInvoker invoker, ICommand command, IReceiver receiver, ILogger logger)
        {
            _invoker = invoker;
            _command = command;
            _receiver = receiver;
            _logger = logger;
        }

        [HttpPost("getAllEvents")]
        public async Task<IActionResult> GetAllEvents(int plannerId, string token)
        {
            try
            {
                List<Event> events = new List<Event>();
                events = await _receiver.GetAllEvents(plannerId);
                return Ok(events);

            }
            catch (Exception e)
            {
                ErrorDto errorDTO = new ErrorDto() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }

        [HttpPost("addEvent")]
        public IActionResult AddEvent([FromBody] AddEventDto addEventDto)
        {
            string username = _receiver.GetUsernameByToken(addEventDto.Token);
            try
            {
                _logger.LogEvent(ELog.INFO, username + " : AddEvent");
                _command = new AddEventCommand(_receiver, addEventDto);
                _invoker.AddAndExecute(_command);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogEvent(ELog.ERROR, username + " : "+e.Message);
                ErrorDto errorDTO = new ErrorDto() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }

        [HttpPut("updateEvent")]
        public IActionResult UpdateEvent([FromBody]UpdateEventDto updateEventDto) {
            string username = _receiver.GetUsernameByToken(updateEventDto.Token);
            try
            {
                _logger.LogEvent(ELog.INFO, username + " : UpdateEvent");
                _command = new UpdateEventCommand(_receiver, updateEventDto);
                _invoker.AddAndExecute(_command);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogEvent(ELog.ERROR, username + " : "+e.Message);
                ErrorDto errorDTO = new ErrorDto() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }
        [HttpDelete("deleteEvent")]
        public IActionResult DeleteEvent(int eventId, string token) {
            string username = _receiver.GetUsernameByToken(token);
            try
            {
                _logger.LogEvent(ELog.INFO, username + " : DeleteEvent");
                _command = new DeleteEventCommand(_receiver, eventId);
                _invoker.AddAndExecute(_command);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogEvent(ELog.ERROR, username + " : "+e.Message);
                ErrorDto errorDTO = new ErrorDto() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }
    }
}
