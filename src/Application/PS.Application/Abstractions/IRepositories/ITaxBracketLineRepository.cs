using PS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Application.Abstractions.IRepositories;
public interface ITaxBracketLineRepository : IBaseRepository<TaxBracketLine>
{
    IQueryable<TaxBracketLine> GetByTaxBracketIdAsync(int taxBracketId);
}
