// ReSharper disable once CheckNamespace
// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
namespace Atc;

public static class NavigationManagerExtendedExtensions
{
    public static bool TryGetQueryString<T>(this NavigationManager navigationManager, string key, out T value)
    {
        ArgumentNullException.ThrowIfNull(navigationManager);

        var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);
        if (QueryHelpers.ParseQuery(uri.Query).TryGetValue(key, out var valueFromQueryString))
        {
            if (typeof(T) == typeof(bool) &&
                bool.TryParse(valueFromQueryString, out var valueAsBool))
            {
                value = (T)(object)valueAsBool;
                return true;
            }

            if (typeof(T) == typeof(decimal) &&
                decimal.TryParse(valueFromQueryString, NumberStyles.Any, CultureInfo.InvariantCulture, out var valueAsDecimal))
            {
                value = (T)(object)valueAsDecimal;
                return true;
            }

            if (typeof(T) == typeof(double) &&
                double.TryParse(valueFromQueryString, NumberStyles.Any, CultureInfo.InvariantCulture, out var valueAsDouble))
            {
                value = (T)(object)valueAsDouble;
                return true;
            }

            if (typeof(T) == typeof(int) &&
                int.TryParse(valueFromQueryString, NumberStyles.Any, CultureInfo.InvariantCulture, out var valueAsInt))
            {
                value = (T)(object)valueAsInt;
                return true;
            }

            if (typeof(T) == typeof(Guid) &&
                Guid.TryParse(valueFromQueryString, out var valueAsGuid))
            {
                value = (T)(object)valueAsGuid;
                return true;
            }

            if (typeof(T) == typeof(string))
            {
                value = (T)(object)valueFromQueryString.ToString();
                return true;
            }
        }

        value = default!;
        return false;
    }
}