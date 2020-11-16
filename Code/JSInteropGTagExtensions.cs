using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace MathBlazor
{
    public static class JSInteropGTagExtensions
    {
        public static ValueTask GTagEvent(this IJSRuntime jsRuntime, string action, string category)
        {
            return jsRuntime.InvokeVoidAsync("GTagEvent", action, category);
        }
    }
}