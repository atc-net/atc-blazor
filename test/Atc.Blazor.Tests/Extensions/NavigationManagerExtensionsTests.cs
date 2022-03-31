namespace Atc.Blazor.Tests.Extensions;

public class NavigationManagerExtensionsTests
{
    [Theory]
    [InlineData(false, 0, "", "hello")]
    [InlineData(true, false, "?myBool=false", "myBool")]
    [InlineData(true, true, "?myBool=true", "myBool")]
    [InlineData(true, 2.5, "?myDouble=2.5", "myDouble")]
    [InlineData(true, 2.6, "?myDouble=2.6", "MYDOUBLE")]
    [InlineData(true, 5, "?myInt=5", "myInt")]
    [InlineData(true, 6, "?myInt=6", "MYINT")]
    [InlineData(true, 7, "?MYINT=7", "myInt")]
    [InlineData(true, 21, "?myBool=true&myInt=21&myDouble=2.6", "myInT")]
    public void TryGetQueryString<T>(bool expectedResult, T expectedValue, string queryStringPart, string queryStringKey)
    {
        // Arrange
        var navigationManager = new FakeNavigationManager("http://localhost/", "http://localhost/" + queryStringPart);

        // Act
        var actualResult = navigationManager.TryGetQueryString<T>(queryStringKey, out var actualValue);

        // Assert
        Assert.Equal(expectedResult, actualResult);
        if (actualResult)
        {
            Assert.Equal(expectedValue, actualValue);
        }
    }
}