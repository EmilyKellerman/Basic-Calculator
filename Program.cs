using CalculatorDomain.Logic;
using CalculatorDomain.Persistence;
using CalculatorDomainDemo;

var store = new FileCalculationStore("Data/calculations.json");
var calculator = new CalculatorService(store);

await calculator.CalculateAsync(
    new CalculationRequest(10, 5, OperationType.Add));

await calculator.CalculateAsync(
    new CalculationRequest(20, 4, OperationType.Divide));

var history = await calculator.GetAllAsync();

foreach (var calc in history)
{
    Console.WriteLine($"{calc.Left} {calc.Operation} {calc.Right} = {calc.Result}");
}