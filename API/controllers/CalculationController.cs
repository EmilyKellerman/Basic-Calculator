using System.Runtime.CompilerServices;
using Basic_Calculator;
using Basic_Calculator.Logic;
using Microsoft.AspNetCore.Mvc;

namespace API.controllers
{
    [ApiController]
    [Route("api/calculations")]
    public class CalculationsController : ControllerBase
    {
        private readonly CalculatorService _calculator;

        public CalculationsController(CalculatorService calculator)
        {
            _calculator = calculator;
        }

        [HttpGet] //GET /api/calculations
        public async Task<ActionResult> GetAll()
        {
            var calculations = await _calculator.GetAllAsync();
            return Ok(calculations);
        }
        
        [HttpPost]
        public async Task<IActionResult> Calculate([FromBody]CreateCalculationDto dto)
        {
                    CalculationRequest request = new CalculationRequest(dto.left, dto.right, dto.operand);
                    var result = await _calculator.CalculateAsync(request);
                    return Ok(request);
        }

    }
}