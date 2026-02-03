using Basic_Calculator.Domain;

namespace Basic_Calculator.Persistence
{
    public interface ICalculationStore
    {
        Task SaveAsync(Calculation calculation);
        Task<IReadOnlyList<Calculation>> LoadAllAsync();
    }
}