using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CalculatorDomainDemo;

/// <summary>
/// Owns calculator behaviour and internal state.
/// 
/// This class:
/// - Performs calculations
/// - Applies business rules
/// - Maintains history
/// 
/// Booking analogy:
/// similar to a booking logic / rules component.
/// </summary>
public class Calculator
{
    /*
     * INTERNAL MUTABLE STATE
     * 
     * This list is intentionally mutable.
     * The calculator changes it over time.
     */
    private readonly List<CalculationRequest> _history = new();

    public string Name { get; }
    public int LastResult { get; private set; }

    public Calculator(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Calculator must have a name.");

        Name = name;
    }

    /*
     * ============================
     * HISTORY ACCESS (COPY)
     * ============================
     * 
     * IMPORTANT DESIGN CHOICE:
     * 
     * We return a COPY of the list,
     * not the internal list itself.
     * 
     * This means:
     * - External code cannot observe live mutation
     * - External code cannot affect internal state
     * - The calculator fully controls its data
     * 
     * Trade-off:
     * - Slightly more memory usage
     * - Stronger safety and predictability
     */
    public IReadOnlyList<CalculationRequest> GetHistory()
    {
        return _history.ToList(); // defensive copy
    }

    /*
     * ============================
     * CORE BEHAVIOUR
     * ============================
     */
    public int Calculate(int a, int b, OperationType operation)
    {
        // Guard clause: fail fast
        if (operation == OperationType.Divide && b == 0)
            throw new InvalidOperationException("Cannot divide by zero.");

        int result = operation switch
        {
            OperationType.Add => a + b,
            OperationType.Subtract => a - b,
            OperationType.Multiply => a * b,
            OperationType.Divide => a / b,
            _ => throw new InvalidOperationException("Invalid operation.")
        };

        // MUTATION happens here (internally only)
        _history.Add(new CalculationRequest(a, b, operation));

        LastResult = result;
        return result;
    }

    // Get Calculation request
        public CalculationRequest GetCalculationRequest()
        {
            
            if (_history.Any())
            {
                throw new CalculationHistoryExcepion();
            }
            else
            {
                return _history.Last();
            }
            
        }

    /*
     * ============================
     * LINQ AS QUESTIONS
     * ============================
     */

    public bool HasUsedDivision()
    {
        return _history.Any(r => r.Operation == OperationType.Divide);
    }

    public CalculationRequest? GetLastCalculation()
    {
        return _history.LastOrDefault();
    }

    public IEnumerable<CalculationRequest> GetByOperation(OperationType operation)
    {
        return _history.Where(r => r.Operation == operation);
    }

    //using linq to get the last multiplication that was done
    public CalculationRequest GetLastMultiplication()
    {
        CalculationRequest req = _history.Last(i => i.Operation == OperationType.Multiply);//last or default then make it nullable so that null value is possible
        return req;
    }

    /*
     * ============================
     * GROUPING WITH DICTIONARY
     * ============================
     */
    public Dictionary<OperationType, List<CalculationRequest>> GroupByOperation()
    {
        var grouped = new Dictionary<OperationType, List<CalculationRequest>>();

        foreach (var request in _history)
        {
            if (!grouped.ContainsKey(request.Operation))
            {
                grouped[request.Operation] = new List<CalculationRequest>();
            }

            grouped[request.Operation].Add(request);
        }

        return grouped;
    }

    //Writing history to files
    public async Task SaveHistoryAsync(string FilePath)
    {
        List<CalculationRequest> snapshot = _history.ToList();
        string json = JsonSerializer.Serialize(snapshot);
        await File.WriteAllTextAsync(FilePath, json);
    }

    //load history from file
    public async Task<List<CalculationRequest>> LoadHistoryAsync(string FilePath)
    {
        List<string> historySnap = new List<string>();
        if (File.Exists(FilePath))
        {
            
            string json = await File.ReadAllTextAsync(FilePath);

            return JsonSerializer.Deserialize<List<CalculationRequest>>(json) ?? new List<CalculationRequest>();
        }
        else
        {
            throw new FileNotFoundException ("History file not found.");
        }
    }

}
