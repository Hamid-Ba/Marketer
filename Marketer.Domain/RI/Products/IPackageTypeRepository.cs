using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Domain;
using Marketer.Application.Contract.ViewModels.Products;
using Marketer.Domain.Entities.Products;

namespace Marketer.Domain.RI.Products
{
    public interface IPackageTypeRepository : IRepository<PackageType>
	{
		Task<PackageTypeVM> GetBy(long id);
		Task<IEnumerable<PackageTypeVM>> GetAll();
		Task<EditPackageTypeVM> GetDetailForEditBy(long id);
	}
}