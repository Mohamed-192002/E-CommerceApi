using AutoMapper;
using ECommerce.Core.DTO;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Date;
using Microsoft.Extensions.FileProviders;

namespace ECommerce.Infrastructure.Repositories
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        private readonly AppDbContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;

        public ProductRepo(AppDbContext context, IFileProvider fileProvider, IMapper mapper) : base(context)
        {
            _context = context;
            _fileProvider = fileProvider;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(CreateProductDTO productDTO)
        {
            var src = "";
            if (productDTO.Image is not null)
            {
                var rootpsth = "/images/products/";
                if (!Directory.Exists("wwwroot" + rootpsth))
                    Directory.CreateDirectory("wwwroot" + rootpsth);

                var productName = $"{Guid.NewGuid()}" + productDTO.Image.FileName;
                src = rootpsth + productName;
                var pictureInfo = _fileProvider.GetFileInfo(src);
                var root = pictureInfo.PhysicalPath;
                using var fileStreem = new FileStream(root, FileMode.Create);

                await productDTO.Image.CopyToAsync(fileStreem);
            }
            var product = _mapper.Map<Product>(productDTO);
            product.ProductPicture = src;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return true;
        }

        // Solve this another time
        public async Task<bool> UpdateAsync(UpdateProductDTO productDTO)
        {
            var currentProduct = await _context.Products.FindAsync(productDTO.Id);
            if (currentProduct is null)
                return false;
            var src = "";
            if (productDTO.Image is not null)
            {
                var rootpsth = "/images/products/";
                if (!Directory.Exists("wwwroot" + rootpsth))
                    Directory.CreateDirectory("wwwroot" + rootpsth);

                var productName = $"{Guid.NewGuid()}" + productDTO.Image.FileName;
                src = rootpsth + productName;
                var pictureInfo = _fileProvider.GetFileInfo(src);
                var root = pictureInfo.PhysicalPath;
                using var fileStreem = new FileStream(root, FileMode.Create);

                await productDTO.Image.CopyToAsync(fileStreem);

                
            }
            if (currentProduct.ProductPicture is not null)
            {
                var OldpictureInfo = _fileProvider.GetFileInfo(currentProduct.ProductPicture);
                var Oldroot = OldpictureInfo.PhysicalPath;
                System.IO.File.Delete(Oldroot);
            }
            var product = _mapper.Map<Product>(productDTO);
            product.ProductPicture = src;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return true;  
        }
    }
}
