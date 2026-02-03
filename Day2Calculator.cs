using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorDemo
{
    public enum OperationType
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public record CalculationRequest(
        int A,
        int B,
        OperationType Operation
    );

    public class Calculator
    {
        private readonly List<CalculationRequest> _history = new();
        public IReadOnlyList<CalculationRequest> History
        {
            get { return _history; }
        }

        public string Name { get; }
        public int LastResult { get; private set; }

        public Calculator(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Calculator must have a name");

            Name = name;
        }

        public int Calculate(int a, int b, OperationType operation)
        {
            // GUARD CLAUSE: Fail fast
            if (operation == OperationType.Divide && b == 0)
                throw new InvalidOperationException("Cannot divide by zero.");

            // Perform calculation
            int result = operation switch
            {
                OperationType.Add => a + b,
                OperationType.Subtract => a - b,
                OperationType.Multiply => a * b,
                OperationType.Divide => a / b,
                _ => throw new InvalidOperationException("Invalid operation")
            };

            // Store request (system memory)
            _history.Add(new CalculationRequest(a, b, operation));

            LastResult = result;
            return result;
        }

        /*
         * ============================
         * LINQ AS QUESTIONS
         * ============================
         */

        // Has division ever been used?
        public bool HasUsedDivision()
        {
            return _history.Any(r => r.Operation == OperationType.Divide);
        }

        // Get the last calculation safely
        public CalculationRequest? GetLastCalculation()
        {
            return _history.LastOrDefault();
        }

        // Filter by operation
        public IEnumerable<CalculationRequest> GetByOperation(OperationType operation)
        {
            return _history.Where(r => r.Operation == operation);
        }

        /*
         * ============================
         * GROUPING WITH DICTIONARY
         * ============================
         * 
         * Dictionary = fast lookup + meaning
         * Key = rule
         * Value = related data
         * 
         * Booking system mapping:
         * - SessionId -> Bookings
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
    }

    /*
     * ============================
     * PROGRAM: Orchestration
     * ============================
     * 
     * Program.cs is NOT the brain.
     * It:
     * - Calls domain logic
     * - Displays output
     * 
     * Booking system mapping:
     * - Controller / API endpoint
     */
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Calculator (End of Day 2) ===");

            var calculator = new Calculator("Advanced Calculator");

            // Simulate multiple requests (like bookings)
            calculator.Calculate(10, 5, OperationType.Add);
            calculator.Calculate(20, 4, OperationType.Divide);
            calculator.Calculate(7, 3, OperationType.Multiply);
            calculator.Calculate(9, 1, OperationType.Divide);

            /*
             * FOREACH: Explicit iteration
             * Shows HOW iteration happens
             */
            Console.WriteLine("\n--- History ---");
            foreach (var r in calculator.History)
            {
                Console.WriteLine($"{r.A} {r.Operation} {r.B}");
            }

            /*
             * LINQ AS QUESTIONS
             */
            Console.WriteLine("\n--- Business Questions ---");
            Console.WriteLine($"Has division been used? {calculator.HasUsedDivision()}");

            var last = calculator.GetLastCalculation();
            if (last != null)
            {
                Console.WriteLine($"Last calculation: {last.A} {last.Operation} {last.B}");
            }

            /*
             * DICTIONARY GROUPING
             */
            Console.WriteLine("\n--- Grouped By Operation ---");
            var grouped = calculator.GroupByOperation();

            foreach (var entry in grouped)
            {
                Console.WriteLine($"\nOperation: {entry.Key}");
                foreach (var req in entry.Value)
                {
                    Console.WriteLine($"  {req.A} {req.Operation} {req.B}");
                }
            }

            Console.WriteLine("\n=== End of Day 2 ===");
        }
    }
}
