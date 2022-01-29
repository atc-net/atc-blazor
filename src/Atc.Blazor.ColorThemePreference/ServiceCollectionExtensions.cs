using Microsoft.Extensions.DependencyInjection;

namespace Atc.Blazor.ColorThemePreference;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddColorThemePreferenceDetector(this IServiceCollection services) =>
        services.AddScoped<IColorThemePreferenceDetector, ColorThemePreferenceDetector>();
}