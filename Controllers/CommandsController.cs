using AutoMapper;
using Commander.Data;
using Commander.DTOs;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Commander.Controllers
{
    [Route("api/commands")] // same if we make it api/[controller]
    [ApiController]
    public class CommandsController : ControllerBase // instead of Controller cuz we won't use views
    {
        private readonly ICommandRepository _commandRepository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepository commandRepository, IMapper mapper)
        {
            _commandRepository = commandRepository;
            _mapper = mapper;
        }

        [HttpGet] // api/commands
        public ActionResult<IEnumerable<CommandReadDTO>> GetAllCommands()
        {
            var commands = _commandRepository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(commands));
        }

        [HttpGet("{id}", Name = "GetCommandByID")] // api/commands/[id]
        public ActionResult<CommandReadDTO> GetCommandByID(int id)
        {
            var command = _commandRepository.GetCommandById(id);
            if (command != null)
            {
                return Ok(_mapper.Map<CommandReadDTO>(command));
            }
            return NotFound();
        }

        [HttpPost] // api/commands
        public ActionResult<CommandReadDTO> CreateCommand(CommandCreateDTO commandCreateDTo)
        {
            if (commandCreateDTo != null)
            {
                var commandModel = _mapper.Map<Command>(commandCreateDTo);
                _commandRepository.CreateCommand(commandModel);
                _commandRepository.SaveChanges();

                var commandReadDTO = _mapper.Map<CommandReadDTO>(commandModel);

                return CreatedAtRoute(nameof(GetCommandByID), new { commandReadDTO.Id }, commandReadDTO);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{Id}")] // api/commands/{Id}
        public ActionResult<CommandReadDTO> UpdateCommand(int Id, CommandUpdateDTO commandUpdateDTO)
        {
            var commandModel = _commandRepository.GetCommandById(Id);
            if (commandModel != null)
            {
                var updatedModel = _mapper.Map(commandUpdateDTO, commandModel);
                _commandRepository.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPatch("{Id}")]// api/commands/{Id}
        public ActionResult PartiallyUpdateCommand(int Id,JsonPatchDocument<CommandUpdateDTO> patchDoc)
        {
            var commandDomainObj = _commandRepository.GetCommandById(Id);
            if (commandDomainObj == null)
            {
                return NotFound();
            }

            var commandDTO = _mapper.Map<CommandUpdateDTO>(commandDomainObj);
            patchDoc.ApplyTo(commandDTO, ModelState);
            if (!TryValidateModel(commandDTO))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map( commandDTO, commandDomainObj);
            _commandRepository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{Id}")] // api/commands/{Id}
        public ActionResult DeleteCommand(int Id)
        {
            var command = _commandRepository.GetCommandById(Id);
            if (command == null)
            {
                return NotFound();
            }
            _commandRepository.DeleteCommand(command);
            _commandRepository.SaveChanges();
            return NoContent();
        }
    }
}