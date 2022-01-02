using Framework.Domain;
using Marketer.Application.Contract.ViewModels.Products;
using Marketer.Domain.Entities.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Domain.RI.Products
{
    public interface ICityRepository : IRepository<City>
    {
        Task<IEnumerable<CityVM>> GetAll();
        Task<EditCityVM> GetDetailForEditBy(long id);
    }
}