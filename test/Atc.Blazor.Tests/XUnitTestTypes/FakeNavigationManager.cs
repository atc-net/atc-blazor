using Microsoft.AspNetCore.Components;

namespace Atc.Blazor.Tests.XUnitTestTypes;

[SuppressMessage("Design", "CA1054:URI-like parameters should not be strings", Justification = "OK.")]
public class FakeNavigationManager : NavigationManager
{
    public FakeNavigationManager()
    {
        Initialize("http://localhost/", "http://localhost/");
    }

    public FakeNavigationManager(string baseUri, string uri)
    {
        Initialize(baseUri, uri);
    }

    protected override void NavigateToCore(string uri, bool forceLoad)
    {
        Uri = ToAbsoluteUri(uri).ToString();
    }
}
