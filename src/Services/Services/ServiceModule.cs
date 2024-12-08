using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Services;

public static class ServiceModule
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataAccessServices(configuration);


        services.AddScoped<IAuth,AuthService>();
        services.AddScoped<IProfileService,ProfileService>();
        services.AddScoped<IFileUploadService,FileUploadService>();
        services.AddScoped<ITagService,TagService>();
        services.AddScoped<IProductCollectionService,ProductCollectionService>();
        services.AddScoped<IProductService,ProductService>();

        services.AddAutoMapper(typeof(TagProfile));
        services.AddAutoMapper(typeof(ProductCollectionProfile));
        services.AddAutoMapper(typeof(ProductProfile));
        services.AddAutoMapper(typeof(ProductTagProfile));

        services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();


        return services;
    }
}
