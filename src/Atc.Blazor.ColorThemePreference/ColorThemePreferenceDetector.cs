using System.Diagnostics.CodeAnalysis;
using Microsoft.JSInterop;

namespace Atc.Blazor.ColorThemePreference
{
    public class ColorThemePreferenceDetector : IColorThemePreferenceDetector
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public ColorThemePreferenceDetector(IJSRuntime jsRuntime)
        {
            moduleTask = new Lazy<Task<IJSObjectReference>>(() => jsRuntime
                .InvokeAsync<IJSObjectReference>(
                    "import",
                    "./_content/Atc.Blazor.ColorThemePreference/ColorThemePreferenceDetector.js")
                .AsTask());
        }

        public async ValueTask<bool> UseLightMode()
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<bool>("useLightMode");
        }

        public async ValueTask<bool> UseDarkMode()
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<bool>("useDarkMode");
        }

        [SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize", Justification = "OK.")]
        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}