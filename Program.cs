using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WasmSpa.Models;
using WasmSpa.Services.Generic;
using WasmSpa.Services.Users;

namespace WasmSpa
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(
                sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

            // Register custom services
            builder.Services
                .AddScoped<LocalStorageService<User>, LocalStorageService<User>>()
                .AddScoped<FakeDataStoreService<UsersStore, User>, FakeDataStoreService<UsersStore, User>>()
                .AddScoped<AuthenticationService, AuthenticationService>()
                .AddScoped<UsersDataStoreService, UsersDataStoreService>()
                .AddScoped<JsAlertService, JsAlertService>();

            await builder.Build().RunAsync();
        }
    }
}