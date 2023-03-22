using Contracts;
using Havit.Blazor.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddExceptionMonitoring(builder.Configuration);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddGrpcServerInfrastructure(assemblyToScanForDataContracts:typeof(Dto).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseGrpcWeb(new GrpcWebOptions() {DefaultEnabled = true});

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();