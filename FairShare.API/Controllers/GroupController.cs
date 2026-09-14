using FairShare.API.DTOs;
using FairShare.API.Mappers.Interfaces;
using FairShare.Core;
using FairShare.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FairShare.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupMapper _groupMapper;

        public GroupsController(IGroupRepository groupRepository, IGroupMapper groupMapper)
        {
            _groupRepository = groupRepository;
            _groupMapper = groupMapper;
        }

        // 1. GET: api/groups/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var group = _groupRepository.GetGroupById(id);

            if (group == null)
                return NotFound(new { message = $"No se encontró el grupo con ID {id}" });

            return Ok(group);
        }

        // 2. GET: api/groups/code/{code}
        [HttpGet("code/{code}")]
        public IActionResult GetByCode(string code)
        {
            var group = _groupRepository.GetGroupByCode(code);

            if (group == null)
                return NotFound(new { message = "Código de grupo no válido" });

            return Ok(group);
        }

        // 3. POST: api/groups
        [HttpPost]
        public IActionResult Create([FromBody] CreateGroupRequest request)
        {
            var newGroup = _groupMapper.ToEntity(request);
            bool success = _groupRepository.CreateGroup(newGroup);

            if (!success)
                return BadRequest(new { message = "No se pudo crear el grupo." });

            return Created();
        }

        // 4. PUT: api/groups/{id}
        [HttpPut]
        public IActionResult Update([FromBody] UpdateGroupRequest request)
        {
            // Creamos la entidad asignándole el ID de la URL
            var groupToUpdate = _groupMapper.ToEntity(request);
            bool success = _groupRepository.UpdateGroup(groupToUpdate);

            if (!success)
                return NotFound(new { message = $"No se pudo actualizar. El grupo con ID {request.Id} no existe." });

            // 204 No Content es la respuesta ideal cuando un PUT funciona correctamente
            return NoContent();
        }

        // 5. DELETE: api/groups/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool success = _groupRepository.DeleteGroup(id);

            if (!success)
                return NotFound(new { message = $"No se pudo borrar. El grupo con ID {id} no existe." });

            // 204 No Content
            return NoContent();
        }
    }
}
