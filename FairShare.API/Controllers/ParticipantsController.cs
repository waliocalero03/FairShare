using FairShare.API.DTOs.Participant;
using FairShare.API.Mappers.Interfaces;
using FairShare.Data.Interfaces;
using FairShare.Data.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FairShare.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantsController : ControllerBase
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IParticipantMapper _mapper;
        private readonly IValidator<CreateParticipantRequest> _createValidator;
        private readonly IValidator<UpdateParticipantRequest> _updateValidator;

        public ParticipantsController(
            IParticipantRepository participantRepository,
            IParticipantMapper mapper,
            IValidator<CreateParticipantRequest> createValidator,
            IValidator<UpdateParticipantRequest> updateValidator,
            IGroupRepository groupRepository)
        {
            _participantRepository = participantRepository;
            _groupRepository = groupRepository;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var participant = _participantRepository.GetParticipantById(id);
            if (participant == null)
                return NotFound(new { message = $"No se encontró el participante con ID {id}" });

            return Ok(participant);
        }

        [HttpGet("group/{groupId}")]
        public IActionResult GetByGroupId(int groupId)
        {
            var participants = _participantRepository.GetParticipantsByGroupId(groupId);
            return Ok(participants);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateParticipantRequest request)
        {
            var validationResult = _createValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                return BadRequest(new { message = "Datos inválidos", errors });
            }

            var existingGroup = _groupRepository.GetGroupById(request.GroupId);
            if (existingGroup == null)
            {
                return NotFound(new { message = $"No se puede añadir el participante. El grupo con ID {request.GroupId} no existe." });
            }

            var newParticipant = _mapper.ToEntity(request);

            bool success = _participantRepository.CreateParticipant(newParticipant);
            if (!success)
                return BadRequest(new { message = "No se pudo crear el participante." });

            return Ok(new { message = "Participante creado con éxito" });
        }

        [HttpPut()]
        public IActionResult Update([FromBody] UpdateParticipantRequest request)
        {
            var validationResult = _updateValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                return BadRequest(new { message = "Datos inválidos", errors });
            }

            var participantToUpdate = _mapper.ToEntity(request);

            bool success = _participantRepository.UpdateParticipant(participantToUpdate);
            if (!success)
                return NotFound(new { message = $"No se pudo actualizar. El participante con ID {request.Id} no existe." });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool success = _participantRepository.DeleteParticipant(id);
            if (!success)
                return NotFound(new { message = $"No se pudo borrar. El participante con ID {id} no existe." });

            return NoContent();
        }
    }
}
