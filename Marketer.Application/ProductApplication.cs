using Framework.Application;
using Marketer.Application.Contract.AI.Products;
using Marketer.Application.Contract.ViewModels.Products;
using Marketer.Domain.Entities.Products;
using Marketer.Domain.RI.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;

        public ProductApplication(IProductRepository productRepository) => _productRepository = productRepository;

        public StatusCheckVM CheckStock(CheckCartItemCountVM command) => _productRepository.CheckStock(command);

        public async Task<OperationResult> Create(CreateProductVM command)
        {
            OperationResult result = new();

            if (_productRepository.Exists(p => p.Code == command.Code))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            if (_productRepository.Exists(p => p.Slug == command.Slug))
                return result.Failed(ApplicationMessage.SlugIsExist);

            if (command.CategoryId <= 0) return result.Failed("لطفا دسته محصول را انتخاب کنید !");
            if (command.BrandId <= 0) return result.Failed("لطفا برند را انتخاب کنید !");

            var picture = Uploader.ImageUploader(command.Picture, $"{command.Slug.Slugify()}", null!);

            var product = new Product(command.BrandId, command.CategoryId,command.PackageTypeId, command.Code, command.Title, picture, command.PictureAlt,
                command.PictureTitle, command.Count, command.ConsumerPrice, command.PurchacePrice,command.PackageValue,
                command.Description, command.ExpiredDate.ToGeorgianDateTime(), command.Slug.Slugify(), command.Keywords, command.MetaDescription);

            await _productRepository.AddEntityAsync(product);
            await _productRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var product = await _productRepository.GetEntityByIdAsync(id);
            if (product is null) return result.Failed(ApplicationMessage.NotExist);

            Uploader.ImageRemover(product.Picture);
            product.Delete();

            await _productRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Edit(EditProductVM command)
        {
            OperationResult result = new();

            var product = await _productRepository.GetEntityByIdAsync(command.Id);

            if (product is null) return result.Failed(ApplicationMessage.NotExist);
            if (_productRepository.Exists(p => p.Code == command.Code && p.Id != command.Id))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            if (_productRepository.Exists(p => p.Slug == command.Slug && p.Id != command.Id))
                return result.Failed(ApplicationMessage.SlugIsExist);

            if (command.CategoryId <= 0) return result.Failed("لطفا دسته محصول را انتخاب کنید !");
            if (command.BrandId <= 0) return result.Failed("لطفا برند را انتخاب کنید !");

            var picture = Uploader.ImageUploader(command.Picture, $"{product.Slug.Slugify()}", product.Picture);

            product.Edit(command.BrandId, command.CategoryId,command.PackageTypeId, command.Code, command.Title, picture, command.PictureAlt,
                command.PictureTitle, command.Count, command.ConsumerPrice, command.PurchacePrice, command.PackageValue,
                command.Description, command.ExpiredDate.ToGeorgianDateTime(), command.Slug.Slugify(), command.Keywords, command.MetaDescription);

            await _productRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<IEnumerable<ProductVM>> GetAll() => await _productRepository.GetAll();

        public async Task<IEnumerable<SelectProductVM>> GetAllForSelection() => await _productRepository.GetAllForSelection();

        public async Task<OperationResult> GetBackProductCount(long id, int count)
        {
            OperationResult result = new();

            var product = await _productRepository.GetEntityByIdAsync(id);
            if (product is null) return result.Failed(ApplicationMessage.NotExist);

            product.AddCount(count);
            await _productRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<EditProductVM> GetDetailForEditBy(long id) => await _productRepository.GetDetailForEditBy(id);

        public async Task<OperationResult> IsCountSatisfyStock(long[] ids, int[] count)
        {
            OperationResult result = new();

            for (int i = 0; i < ids.Length; i++)
            {
                var product = await _productRepository.GetEntityByIdAsync(ids[i]);
                if (product is null) return result.Failed(ApplicationMessage.GoesWrong);

                if (product.Count < count[i]) return result.Failed($"محصول {product.Title} کمتر از تعداد درخواستی در انبار هست");
            }

            return result.Succeeded();
        }

        public async Task<OperationResult> IsProductExistInStock(long id)
        {
            OperationResult result = new();

            var product = await _productRepository.GetEntityByIdAsync(id);

            if (product is null) return result.Failed(ApplicationMessage.NotExist);

            if (!product.IsInStock()) return result.Failed("محصول در انبار موجود نمی باشد");

            return result.Succeeded();
        }

        public async Task<OperationResult> IsProductExistInStock(string slug)
        {
            OperationResult result = new();

            var product = await _productRepository.GetBy(slug);

            if (product is null) return result.Failed(ApplicationMessage.NotExist);

            if (!product.IsInStock()) return result.Failed("محصول در انبار موجود نمی باشد");

            return result.Succeeded();
        }
    }
}