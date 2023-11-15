// ReSharper disable once CheckNamespace
// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
// ReSharper disable SuggestBaseTypeForParameter
namespace Atc;

public static class ComponentBaseExtensions
{
    public static void SetPropertiesWithDecoratedQueryStringParameterFromQueryString<T>(this T component, NavigationManager navigationManager)
        where T : ComponentBase
    {
        ArgumentNullException.ThrowIfNull(navigationManager);

        if (!Uri.TryCreate(navigationManager.Uri, UriKind.RelativeOrAbsolute, out var uri))
        {
            throw new InvalidOperationException("The current url is not a valid URI. Url: " + navigationManager.Uri);
        }

        var queryString = QueryHelpers.ParseQuery(uri.Query);
        foreach (var property in GetPublicAndNonPublicProperties<T>())
        {
            var parameterName = GetQueryStringParameterName(property);
            if (parameterName is null)
            {
                continue;
            }

            if (!queryString.TryGetValue(parameterName, out var value))
            {
                continue;
            }

            var convertedValue = Convert.ChangeType(value[0], property.PropertyType, CultureInfo.InvariantCulture);
            property.SetValue(component, convertedValue);
        }
    }

    public static void UpdateQueryStringFromPropertiesWithDecoratedQueryStringParameter<T>(this T component, NavigationManager navigationManager)
        where T : ComponentBase
    {
        ArgumentNullException.ThrowIfNull(navigationManager);

        if (!Uri.TryCreate(navigationManager.Uri, UriKind.RelativeOrAbsolute, out var uri))
        {
            throw new InvalidOperationException("The current url is not a valid URI. Url: " + navigationManager.Uri);
        }

        var parameters = QueryHelpers.ParseQuery(uri.Query);
        foreach (var property in GetPublicAndNonPublicProperties<T>())
        {
            var parameterName = GetQueryStringParameterName(property);
            if (parameterName is null)
            {
                continue;
            }

            var value = property.GetValue(component);
            if (value is null)
            {
                parameters.Remove(parameterName);
            }
            else
            {
                var convertedValue = Convert.ToString(value, CultureInfo.InvariantCulture);
                parameters[parameterName] = convertedValue;
            }
        }

        var newUri = uri.GetComponents(UriComponents.Scheme | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.UriEscaped);
        foreach (var (key, stringValues) in parameters)
        {
            foreach (var value in stringValues)
            {
                newUri = QueryHelpers.AddQueryString(newUri, key, value!);
            }
        }

        navigationManager.NavigateTo(newUri);
    }

    [SuppressMessage("Major Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields", Justification = "OK - By design.")]
    private static PropertyInfo[] GetPublicAndNonPublicProperties<T>()
        => typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

    private static string? GetQueryStringParameterName(PropertyInfo propertyInfo)
    {
        var attribute = propertyInfo.GetCustomAttribute<QueryStringParameterAttribute>();
        if (attribute is null)
        {
            return null;
        }

        return !string.IsNullOrEmpty(attribute.Name)
            ? attribute.Name
            : propertyInfo.Name;
    }
}