using PS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Application.Abstractions.IRepositories;
public interface ICountryRepository : IBaseRepository<Country>
{
    Task<Country> ADOFindByIdAsync(int id);
    Task<Country> FindByIdWithRelatedDataAsync(int id);
    IQueryable<Country> FindWithRelatedDatasAsync(List<int> countryIds);
    IQueryable<Country> FindWithRelatedDatasAsync();


}
