namespace WebApi.Config;

public static class AutoMapperConfig
{
    public static void AddAutoMapperConfig(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}
