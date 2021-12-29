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

            var product = new Product(command.BrandId, command.CategoryId, command.Code, command.Title, picture, command.PictureAlt,
                command.PictureTitle, command.Count, command.EachBoxCount, command.ConsumerPrice, command.PurchacePrice, command.Weight,
                command.ExpiredDate.ToGeorgianDateTime(), command.Slug.Slugify(), command.Keywords, command.MetaDescription);

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

            product.Edit(command.BrandId, command.CategoryId, command.Code, command.Title, picture, command.PictureAlt,
                command.PictureTitle, command.Count ,command.EachBoxCount, command.ConsumerPrice, command.PurchacePrice, command.Weight,
                command.ExpiredDate.ToGeorgianDateTime(),command.Slug.Slugify(), command.Keywords, command.MetaDescription);

            await _productRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<IEnumerable<ProductVM>> GetAll() => await _productRepository.GetAll();

        public async Task<IEnumerable<SelectProductVM>> GetAllForSelection() => await _productRepository.GetAllForSelection();

        public async Task<EditProductVM> GetDetailForEditBy(long id) => await _productRepository.GetDetailForEditBy(id);
        
    }
}