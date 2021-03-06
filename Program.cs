using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace MathBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Configuration.Add(
                new MemoryConfigurationSource 
                {
                    InitialData = new Dictionary<string, string>()
                    {
                        { "Version", ThisAssembly.Version },
                        { "InformationalVersion", ThisAssembly.InformationalVersion }
                    }
                });

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

            builder.Services.AddScoped<TablesExerciseFactory>();
            builder.Services.AddScoped<ExerciseHistory>();
            builder.Services.AddScoped<ExerciseRepeater>();
            builder.Services.AddScoped<RepeatPriorityCalculator>((sp) => new RepeatPriorityCalculator(5000));

            var host = builder.Build();
            
            var jsInterop = host.Services.GetRequiredService<IJSRuntime>();
            var language = await jsInterop.InvokeAsync<string>("blazorCulture.get");
            if (language != null) {
                var culture = new CultureInfo(language);
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }
            
            await host.RunAsync();
        }
    }
}
