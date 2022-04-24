using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Application;
using Marketer.Application.Contract.ViewModels.Products;

namespace Marketer.Application.Contract.AI.Products
{
	public interface IPackageTypeApplication
	{
		Task<PackageTypeVM> GetBy(long id);
		Task<IEnumerable<PackageTypeVM>> GetAll();
		Task<EditPackageTypeVM> GetDetailForEditBy(long id);
		Task<OperationResult> Edit(EditPackageTypeVM command);
		Task<OperationResult> Create(CreatePackageTypeVM command);
		Task<OperationResult> Delete(DeletePackageTypeVM command);
	}
}

