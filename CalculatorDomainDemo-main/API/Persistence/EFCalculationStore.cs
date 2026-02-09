using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CalculatorDomainDemo.Domain;
using CalculatorDomainDemo.Persistence;
using API.Data;

namespace API.Persistence
{
    public class EFCalculationStore : ICalculationStore
    {
        private readonly CalculatorDbContext _context;

        public EFCalculationStore(CalculatorDbContext context)
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
            return await _context.Calculations
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }
    }
}