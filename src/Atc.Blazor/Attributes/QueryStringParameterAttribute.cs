// ReSharper disable once CheckNamespace
namespace Atc;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class QueryStringParameterAttribute : Attribute
{
    public QueryStringParameterAttribute()
    {
        Name = string.Empty;
    }

    public QueryStringParameterAttribute(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Name of the query string parameter. It uses the property name by default.
    /// </summary>
    public string Name { get; }
}
