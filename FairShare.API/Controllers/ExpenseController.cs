using FairShare.API.DTOs.Expense;
using FairShare.API.Mappers.Interfaces;
using FairShare.Core;
using FairShare.Data.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FairShare.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IExpenseMapper _expenseMapper;
        private readonly IValidator<CreateExpenseRequest> _createValidator;
        private readonly IValidator<UpdateExpenseRequest> _updateValidator;

        public ExpensesController(
            IExpenseRepository expenseRepository,
            IExpenseMapper expenseMapper,
            IValidator<CreateExpenseRequest> createValidator,
            IValidator<UpdateExpenseRequest> updateValidator)
        {
            _expenseRepository = expenseRepository;
            _expenseMapper = expenseMapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        // 1. GET: api/expenses/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var expense = _expenseRepository.GetExpenseById(id);

            if (expense == null)
                return NotFound(new { message = $"No se encontró el gasto con ID {id}" });

            var response = _expenseMapper.ToResponse(expense);
            return Ok(response);
        }

        // 2. GET: api/expenses/group/{groupId}
        [HttpGet("group/{groupId}")]
        public IActionResult GetByGroupId(int groupId)
        {
            var expenses = _expenseRepository.GetExpensesByGroupId(groupId);

            if (expenses == null || !expenses.Any())
                return NotFound(new { message = $"No se encontraron gastos para el grupo con ID {groupId}" });

            var responses = expenses.Select(e => _expenseMapper.ToResponse(e)).ToList();
            return Ok(responses);
        }

        // 3. POST: api/expenses
        [HttpPost]
        public IActionResult Create([FromBody] CreateExpenseRequest request)
        {
            var validationResult = _createValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                return BadRequest(new { message = "Datos inválidos", errors });
            }

            var newExpense = _expenseMapper.ToEntity(request);
            int newId = _expenseRepository.CreateExpense(newExpense);

            if (newId <= 0)
                return BadRequest(new { message = "No se pudo crear el gasto." });

            return Created($"api/expenses/{newId}", new { id = newId, message = "Gasto creado exitosamente" });
        }

        // 4. PUT: api/expenses/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateExpenseRequest request)
        {
            if (id != request.Id)
                return BadRequest(new { message = "El ID del gasto en la URL no coincide con el del cuerpo de la solicitud." });

            var validationResult = _updateValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                return BadRequest(new { message = "Datos inválidos", errors });
            }

            var expenseToUpdate = _expenseMapper.ToEntity(request);
            bool success = _expenseRepository.UpdateExpense(expenseToUpdate);

            if (!success)
                return BadRequest(new { message = "No se pudo actualizar el gasto." });

            return Ok(new { message = "Gasto actualizado exitosamente" });
        }

        // 5. DELETE: api/expenses/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var expense = _expenseRepository.GetExpenseById(id);

            if (expense == null)
                return NotFound(new { message = $"No se encontró el gasto con ID {id}" });

            bool success = _expenseRepository.DeleteExpense(id);

            if (!success)
                return BadRequest(new { message = "No se pudo eliminar el gasto." });

            return Ok(new { message = "Gasto eliminado exitosamente" });
        }
    }
}
