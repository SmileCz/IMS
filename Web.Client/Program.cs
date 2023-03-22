using Contracts;
using Havit.Blazor.Grpc.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Web.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("Web.Server",
    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
    .CreateClient("Web.Server"));
    


builder.Services.AddGrpcClientInfrastructure(assemblyToScanForDataContracts:typeof(Dto).Assembly);

Resources.ResourcesServiceCollectionInstaller.AddGeneratedResourceWrappers(builder.Services);

await builder.Build().RunAsync();