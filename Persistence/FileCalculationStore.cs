using System.Text.Json;
using CalculatorDomainDemo.Domain;
using CalculatorDomainDemo.Persistence;

namespace CalculatorDomain.Persistence
{
    public class FileCalculationStore : ICalculationStore
    {
        private readonly string _filePath;

        public FileCalculationStore(string filePath)
        {
            _filePath = filePath;
        }

        public async Task SaveAsync(Calculation calculation)
        {
            var calculations = (await LoadAllAsync()).ToList();
            calculations.Add(calculation);

            var json = JsonSerializer.Serialize(calculations);
            await File.WriteAllTextAsync(_filePath, json);
        }

        public async Task<IReadOnlyList<Calculation>> LoadAllAsync()
        {
            if (!File.Exists(_filePath))
                return new List<Calculation>();

            var json = await File.ReadAllTextAsync(_filePath);

            return JsonSerializer.Deserialize<List<Calculation>>(json)
                   ?? new List<Calculation>();
        }
    }
}