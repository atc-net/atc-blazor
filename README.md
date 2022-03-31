[![NuGet Version](https://img.shields.io/nuget/v/Atc.Blazor.ColorThemePreference.svg?logo=nuget&style=for-the-badge)](https://www.nuget.org/packages/Atc.Blazor.ColorThemePreference)

# Atc.Blazor

This repository contains packages with components for Blazor application:

| Package | Description |
|---|---|
| Atc.Blazor.ColorThemePreference | A library for detecting the user preferred color theme |

## Requirements

* [.NET 6 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

## Get started Atc.Blazor

### Installation for Atc.Blazor

```powershell
Install-Package Atc.Blazor
```

### How to Use `NavigationManager.TryGetQueryString`

```csharp
int myInt = 0;

NavigationManager.TryGetQueryString<int>("myKey", out var myInt)
```

### How to Use `QueryStringParameterAttribute` and `SetPropertiesWithDecoratedQueryStringParameterFromQueryString`

```csharp
@page "/"
@inject NavigationManager NavigationManager

<div>My age is: @Age</div>

@code
{
    [QueryStringParameter]
    public int Age { get; set; }

    public override Task SetParametersAsync(ParameterView parameters) // Overload from Blazor components lifecycle
    {
        this.SetPropertiesWithDecoratedQueryStringParameterFromQueryString(NavigationManager); // Bind from url-qyery-parameter 'age' to property 'Age'
        return base.SetParametersAsync(parameters);
    }
}
```

```csharp
@page "/"
@inject NavigationManager NavigationManager

<div>My age is: @Age</div>

@code
{
    [QueryStringParameter("myAge")]
    public int Age { get; set; }

    public override Task SetParametersAsync(ParameterView parameters) // Overload from Blazor components lifecycle
    {
        this.SetPropertiesWithDecoratedQueryStringParameterFromQueryString(NavigationManager); // Bind from url-qyery-parameter 'myAge' to property 'Age'
        return base.SetParametersAsync(parameters);
    }
}
```

### How to Use `QueryStringParameterAttribute` and `UpdateQueryStringFromPropertiesWithDecoratedQueryStringParameter`

```csharp
@page "/"
@inject NavigationManager NavigationManager

<button type="button" @onclick="UpdateQueryStringWithAge(21)">Update url</button>

@code
{
    [QueryStringParameter]
    public int Age { get; set; }

    public void UpdateQueryStringWithAge(int age)
    {
        this.Age = age;
        this.UpdateQueryString(NavigationManager);
    }
}
```

## Get started Atc.Blazor.ColorThemePreference

### Installation for Atc.Blazor.ColorThemePreference

```powershell
Install-Package Atc.Blazor.ColorThemePreference
```

### How to Setup

Modify `Program.cs` by adding to the service builder:

```csharp
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddColorThemePreferenceDetector(); // Require package: Atc.Blazor.ColorThemePreference

await builder.Build().RunAsync();
```

Modify `index.html` by adding:

```html
<script src="_content/Atc.Blazor.ColorThemePreference/ColorThemePreferenceDetector.js" type="module"></script>
```

### How to Use

```csharp
@inject IColorThemePreferenceDetector colorThemePreferenceDetector;

<p>Use Light-Mode: @useLightMode</p>
<p>Use Dark-Mode: @useDarkMode</p>

@code
{
    private bool useLightMode;
    private bool useDarkMode;

    protected override async Task OnInitializedAsync()
    {
        useLightMode = await colorThemePreferenceDetector.UseLightMode();
        useDarkMode = await colorThemePreferenceDetector.UseDarkMode();

        await base.OnInitializedAsync();
    }
}
```

## How to contribute

[Contribution Guidelines](https://atc-net.github.io/introduction/about-atc#how-to-contribute)

[Coding Guidelines](https://atc-net.github.io/introduction/about-atc#coding-guidelines)