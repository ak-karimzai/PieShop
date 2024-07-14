using Microsoft.EntityFrameworkCore;

namespace PieShop.Models;

public class PieRepository : IPieRepository
{
    private readonly PieShopDbContext _dbContext;

    public PieRepository(PieShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Pie> AllPies => _dbContext.Pies
                .Include(pie => pie.Category);

    public IEnumerable<Pie> PiesOfTheWeek => _dbContext.Pies
                .Include(pie => pie.Category)
                .Where(pie => pie.IsPieOfTheWeek);

    public Pie? GetPieById(int pieId) => _dbContext.Pies
            .FirstOrDefault(pie => pie.PieId == pieId);

    public IEnumerable<Pie> SearchPies(string searchQuery)
    {
        return _dbContext.Pies
            .Where(p => p.Name.Contains(searchQuery));
    }
}