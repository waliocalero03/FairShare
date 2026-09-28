using FairShare.API.DTOs.ExpenseSplit;
using FairShare.API.Mappers.Interfaces;
using FairShare.Core;
using FairShare.Data.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FairShare.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseSplitsController : ControllerBase
    {
        private readonly IExpenseSplitRepository _expenseSplitRepository;
        private readonly IExpenseSplitMapper _expenseSplitMapper;
        private readonly IValidator<CreateExpenseSplitRequest> _createValidator;
        private readonly IValidator<UpdateExpenseSplitRequest> _updateValidator;

        public ExpenseSplitsController(
            IExpenseSplitRepository expenseSplitRepository,
            IExpenseSplitMapper expenseSplitMapper,
            IValidator<CreateExpenseSplitRequest> createValidator,
            IValidator<UpdateExpenseSplitRequest> updateValidator)
        {
            _expenseSplitRepository = expenseSplitRepository;
            _expenseSplitMapper = expenseSplitMapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        // 1. GET: api/expensesplits/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var expenseSplit = _expenseSplitRepository.GetExpenseSplitById(id);

            if (expenseSplit == null)
                return NotFound(new { message = $"No se encontró la división de gasto con ID {id}" });

            var response = _expenseSplitMapper.ToResponse(expenseSplit);
            return Ok(response);
        }

        // 2. GET: api/expensesplits/expense/{expenseId}
        [HttpGet("expense/{expenseId}")]
        public IActionResult GetByExpenseId(int expenseId)
        {
            var expenseSplits = _expenseSplitRepository.GetSplitsByExpenseId(expenseId);

            if (expenseSplits == null || !expenseSplits.Any())
                return NotFound(new { message = $"No se encontraron divisiones de gasto para el gasto con ID {expenseId}" });

            var responses = expenseSplits.Select(es => _expenseSplitMapper.ToResponse(es)).ToList();
            return Ok(responses);
        }

        // 3. GET: api/expensesplits/participant/{participantId}
        [HttpGet("participant/{participantId}")]
        public IActionResult GetByParticipantId(int participantId)
        {
            var expenseSplits = _expenseSplitRepository.GetSplitsByParticipantId(participantId);

            if (expenseSplits == null || !expenseSplits.Any())
                return NotFound(new { message = $"No se encontraron divisiones de gasto para el participante con ID {participantId}" });

            var responses = expenseSplits.Select(es => _expenseSplitMapper.ToResponse(es)).ToList();
            return Ok(responses);
        }

        // 4. POST: api/expensesplits
        [HttpPost]
        public IActionResult Create(CreateExpenseSplitRequest request)
        {
            var validationResult = _createValidator.Validate(request);

            if (!validationResult.IsValid)
                return BadRequest(new { errors = validationResult.Errors.Select(e => e.ErrorMessage) });

            var expenseSplit = _expenseSplitMapper.ToEntity(request);
            var result = _expenseSplitRepository.CreateExpenseSplit(expenseSplit);

            if (!result)
                return BadRequest(new { message = "Error al crear la división de gasto" });

            var createdExpenseSplit = _expenseSplitRepository.CreateExpenseSplit(expenseSplit);

            return Created();
        }

        // 5. PUT: api/expensesplits/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateExpenseSplitRequest request)
        {
            if (id != request.Id)
                return BadRequest(new { message = "El ID en la URL no coincide con el ID en el cuerpo de la solicitud" });

            var validationResult = _updateValidator.Validate(request);

            if (!validationResult.IsValid)
                return BadRequest(new { errors = validationResult.Errors.Select(e => e.ErrorMessage) });

            var existingExpenseSplit = _expenseSplitRepository.GetExpenseSplitById(id);

            if (existingExpenseSplit == null)
                return NotFound(new { message = $"No se encontró la división de gasto con ID {id}" });

            var expenseSplit = _expenseSplitMapper.ToEntity(request);
            var result = _expenseSplitRepository.UpdateExpenseSplit(expenseSplit);

            if (!result)
                return BadRequest(new { message = "Error al actualizar la división de gasto" });

            var updatedExpenseSplit = _expenseSplitRepository.GetExpenseSplitById(id);

            if (updatedExpenseSplit == null)
            {
                return BadRequest(new { message = "Error al obtener la división de gasto actualizada" });
            }

            var response = _expenseSplitMapper.ToResponse(updatedExpenseSplit);
            return Ok(response);
        }

        // 6. DELETE: api/expensesplits/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var expenseSplit = _expenseSplitRepository.GetExpenseSplitById(id);

            if (expenseSplit == null)
                return NotFound(new { message = $"No se encontró la división de gasto con ID {id}" });

            var result = _expenseSplitRepository.DeleteExpenseSplit(id);

            if (!result)
                return BadRequest(new { message = "Error al eliminar la división de gasto" });

            return NoContent();
        }
    }
}
