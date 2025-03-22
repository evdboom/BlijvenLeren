using Azure.Data.Tables;
using Azure.Identity;
using BlijvenLerenApi.Json;
using BlijvenLerenApi.UseCases.LearningResources;
using BlijvenLerenApi.UseCases.Shared;

namespace BlijvenLerenApi;

public static class ServiceRegistration
{
    private const string TableEndpoint = "https://{0}.table.core.windows.net/";
    private const string StorageAccountKey = "STORAGE_ACCOUNT_NAME";
    public static IServiceCollection RegisterApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var storageAccount = configuration.GetValue<string>(StorageAccountKey);

        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.TypeInfoResolverChain.Insert(0, new LearningRequestContext());
            options.SerializerOptions.TypeInfoResolverChain.Insert(0, new LearningResponseContext());
        });

        services
            .AddScoped(p => new TableServiceClient(new Uri(string.Format(TableEndpoint, storageAccount)), new DefaultAzureCredential()))
            .AddSingleton<ITimeProvider, UseCases.Shared.TimeProvider>()
            .AddHttpContextAccessor();

        services
            .AddScoped<IEndpoint, GetLearningResourcesQueryEndpoint>()
            .AddScoped<GetLearningResourcesQueryHandler>();

        services
            .AddScoped<IEndpoint, AddLearningResourcesCommandEndpoint>()
            .AddScoped<AddLearningResourcesCommandHandler>();

        return services;
    }
}
