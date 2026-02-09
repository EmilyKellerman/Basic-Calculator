using System.ComponentModel;

public class EFCalculationStore : ICalculationStore
{
    private readonly CalculatorDbContext _context;
    public EFCalculationStore(CalculationDbContext context)
    {
        _context = context;
    }
    public async Task SaveAsync(Calculation calculation)
    {
        _context.Calculations.Add(calculation);
        await _context.SaveChangesAsync();
    }
    public async Task<IReadOnlyList<Calculation>> LoadAllAsync()
    {
        return await _context.Calculations.OrderByDescending(c => c.CreatedAt).ToListAsync();
    }
}