using System.Windows;

namespace CryptoApp.Services;

public static class ThemeManager
{
    public static void ChangeTheme(string themeName)
    {
        string themePath = $"Themes/{themeName}.xaml";
        var themeUri = new Uri(themePath, UriKind.Relative);

        ResourceDictionary newTheme = new ResourceDictionary { Source = themeUri };
        Application.Current.Resources.MergedDictionaries.Clear();
        Application.Current.Resources.MergedDictionaries.Add(newTheme);
    }
}
