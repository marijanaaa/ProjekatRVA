using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjekatRVA.Models;
using ProjekatRVA.Models.Dto;
using ProjekatRVA.Models.Dto.UserDto;
using ProjekatRVA.Receivers;
using ProjekatRVA.Service.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjekatRVA.Logger;
using ProjekatRVA.Enums;
using ProjekatRVA.Models.Dto.PlannerDto;
using ProjekatRVA.Commands.Interfaces;
using ProjekatRVA.Invokers;
using ProjekatRVA.Commands.Providers;

namespace ProjekatRVA.Controllers
{
    [Route("api/planner")]
    [ApiController]
    public class PlannerController : ControllerBase
    {
        private readonly ILogger _logger;
        private IInvoker _invoker;
        private ICommand _command;
        private IReceiver _receiver;
        public PlannerController(IInvoker invoker, ICommand command, IReceiver receiver, ILogger logger)
        {
            _invoker = invoker;
            _command = command;
            _receiver = receiver;
            _logger = logger;
        }

        [HttpPost("getAllPlanners")]
        public async Task<IActionResult> GetAllPlanners([FromBody]TokenDto tokenDto)
        {
            try
            {
                List<PlannerDto> retlist = new List<PlannerDto>();
                retlist = await _receiver.GetAllPlanners(tokenDto.Token);
                return Ok(retlist);
            }
            catch (Exception e)
            {
                ErrorDto errorDTO = new ErrorDto() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }

        [HttpPost("addNewPlanner")]
        public IActionResult AddNewPlanner([FromBody] AddNewPlannerDto dto)
        {
            string username = _receiver.GetUsernameByToken(dto.Token);
            try
            { 
                _logger.LogEvent(ELog.INFO, username + " : AddNewPlanner");
                _command = new AddNewPlannerCommand(_receiver, dto);
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
        
        [HttpPut("updatePlanner")]
        public IActionResult UpdatePlanner([FromBody] EditPlannerDto dto) {
            string username = _receiver.GetUsernameByToken(dto.Token); ;
            try
            {
                _logger.LogEvent(ELog.INFO, username + " : UpdatePlanner");
                _command = new UpdatePlannerCommand(_receiver, dto);
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
        
        [HttpDelete("deletePlanner")]
        public IActionResult DeletePlanner([FromBody]DeleteDto deleteDto) {
            string username = _receiver.GetUsernameByToken(deleteDto.Token);
            try
            {
                _logger.LogEvent(ELog.INFO, username + " : DeletePlanner");
                _command = new DeletePlannerCommand(_receiver, deleteDto);
                _invoker.AddAndExecute(_command);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogEvent(ELog.INFO, username + " : "+e.Message);
                ErrorDto errorDTO = new ErrorDto() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }

        [HttpPost("duplicatePlanner")]
        public IActionResult DuplicatePlanner([FromBody]DuplicateDto duplicateDto) {
            string username = _receiver.GetUsernameByToken(duplicateDto.Token);
            try
            {
                _logger.LogEvent(ELog.INFO, username + " : DuplicatePlanner");
                _command = new DuplicatePlannerCommand(_receiver, duplicateDto);
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

        [HttpPost("searchPlanners")]
        public IActionResult SearchPlanners([FromBody]SearchDto searchDto) {
            string username = _receiver.GetUsernameByToken(searchDto.Token);
            try
            {
                _logger.LogEvent(ELog.INFO, username + " : SearchPlanners");
                _command = new SearchPlannerCommand(_receiver, searchDto);
                _invoker.AddAndExecute(_command);
                List<PlannerDto> planners = _receiver.SearchPlanners(searchDto.PlannerName);
                return Ok(planners);
            }
            catch (Exception e)
            {
                _logger.LogEvent(ELog.ERROR, username + " : "+e.Message);
                ErrorDto errorDTO = new ErrorDto() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }

        [HttpPost("getPlanner")]
        public IActionResult GetPlanner(GetPlannerDto getPlannerDto) {
            string username = _receiver.GetUsernameByToken(getPlannerDto.Token);
            try
            {
                PlannerDto plannerDto = _receiver.GetPlannerById(getPlannerDto.PlannerId);
                return Ok(plannerDto);
            }
            catch (Exception e)
            {
                ErrorDto errorDTO = new ErrorDto() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }
    }
}
