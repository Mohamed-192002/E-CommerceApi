using AutoMapper;
using ECommerce.Core.DTO;
using Microsoft.Extensions.FileProviders;
using System.Linq.Expressions;

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
        public async Task<IEnumerable<Product>> GetAllAsync(int? pageNumber, int? pageSize,
            Expression<Func<Product, bool>> SearchByCategoryId = null, string SearchByName = null, string[] includes = null
            , Expression<Func<Product, object>> orderBy = null, string orderByDirection = "ASC")
        {
            IQueryable<Product> query = _context.Set<Product>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            if (orderBy != null)
            {
                if (orderByDirection == "ASC")
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }


            // search by name
            if (SearchByName is not null)
                query = query.Where(p => p.Name.ToLower().Contains(SearchByName.ToLower()));



            // filter by category Id
            if (SearchByCategoryId is not null)
                query = query.Where(SearchByCategoryId);



            query = query.Skip((int)(pageSize * (pageNumber - 1))).Take((int)pageSize);

            return query;
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
