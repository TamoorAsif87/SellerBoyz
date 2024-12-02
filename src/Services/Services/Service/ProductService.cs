
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Services.Service;

public class ProductService(ApplicationDbContext context,IMapper mapper,IFileUploadService fileUploadService,IWebHostEnvironment webHostEnvironment) : IProductService
{
 
    public async Task CreateAsync(ProductDto productDto)
    {
        var product = mapper.Map<Product>(productDto);
        context.Products.Add(product);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid Id)
    {
        var product = await GetByIdAsync(Id);
        context.Products.Remove(new Product { Id = product.Id });
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await context.Products
            .AsNoTracking()
            .Include(p => p.ProductCollection).Include(p => p.Tags)
            .ToListAsync();
        return mapper.Map<List<ProductDto>>(products);  
    }

    public async Task<ProductDto> GetByIdAsync(Guid Id)
    {
       var product = await context.Products
            .AsNoTracking().Include(p => p.ProductCollection)
            .Include(p => p.Tags)!.ThenInclude(p => p.Tags)
            .SingleOrDefaultAsync(t => t.Id == Id);
       if (product == null) throw new Exception("Not found product");
       return mapper.Map<ProductDto>(product);
    }

    public async Task UpdateAsync(ProductDto productDto, Guid Id)
    {
        var product = await context.Products.AsNoTracking().SingleOrDefaultAsync(t => t.Id == Id);
        if (product == null) throw new Exception("Not found product");

        var  updatedProduct = mapper.Map<Product>(productDto);
        updatedProduct.Images = product.Images;
        updatedProduct.Tags = product.Tags;

        context.Products.Update(updatedProduct);
        await context.SaveChangesAsync();
    }


    public async Task AddProductImage(Guid ProductId, IFormFile formFile)
    {
        var product = await GetProduct(ProductId);

        if(formFile != null)
        {
            product.Images.Add(fileUploadService.ImageUpload(formFile,"images"));
        }

        context.Products.Update(product);
        await context.SaveChangesAsync();
    }


    public async Task RemoveProductImage(Guid ProductId, string ImageName)
    {
        var product = await GetProduct(ProductId);
        product.Images = product.Images.Where(p => p != ImageName).ToList();

        var imgPath = webHostEnvironment.WebRootPath + $@"\images\{ImageName.Split('/')[^1]}";

        if (File.Exists(imgPath))
        {
            File.Delete(imgPath);
        }


        context.Products.Update(product);
        await context.SaveChangesAsync();
    }

    public async Task AddTag(Guid TagId, Guid ProductId)
    {
        context.ProductTags.Add(new ProductTag { ProductId = ProductId,TagsId = TagId });
        await context.SaveChangesAsync();
    }

    public async Task RemoveTag(Guid TagId, Guid ProductId)
    {
        var tag = await context.ProductTags.SingleOrDefaultAsync(t => t.TagsId == TagId && t.ProductId == ProductId);
        if (tag == null) throw new Exception("No product Tag found");

        context.ProductTags.Remove(tag);
        await context.SaveChangesAsync();
    }

    private async Task<Product> GetProduct(Guid ProductId)
    {
        var product = await context.Products.AsNoTracking().SingleOrDefaultAsync(p => p.Id == ProductId);
        if (product == null) throw new Exception("No Product found");
        return product; 
    }
}
