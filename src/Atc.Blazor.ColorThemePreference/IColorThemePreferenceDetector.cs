namespace Atc.Blazor.ColorThemePreference;

public interface IColorThemePreferenceDetector : IAsyncDisposable
{
    ValueTask<bool> UseLightMode();

    ValueTask<bool> UseDarkMode();
}