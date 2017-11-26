﻿using System;
using System.Windows;
using MahApps.Metro;
using Neo.Gui.Base.Extensions;
using Neo.Gui.Base.Helpers.Interfaces;
using Neo.Gui.Base.Theming;
using Neo.Gui.Wpf.Extensions;
using Neo.Gui.Wpf.Properties;

namespace Neo.Gui.Wpf.Helpers
{
    public class ThemeHelper : IThemeHelper
    {
        private const string CustomAccentKey = "CustomAccent";

        #region Resource Key Constants

        private const string HighlightColorKey = "ThemeHighlightColor";
        private const string AccentBaseColorKey = "ThemeAccentBaseColor";
        private const string AccentColor1Key = "ThemeAccentColor";
        private const string AccentColor2Key = "ThemeAccentColor2";
        private const string AccentColor3Key = "ThemeAccentColor3";
        private const string AccentColor4Key = "ThemeAccentColor4";
        private const string WindowBorderColorKey = "ThemeWindowBorderColor";

        #endregion Resource Key Constants

        #region IThemeHelper implementation

        public NeoGuiTheme CurrentTheme { get; private set; }

        public void LoadTheme()
        {
            // Add custom accent resource dictionary to the ThemeManager, contains default color values
            ThemeManager.AddAccent(CustomAccentKey, new Uri("pack://application:,,,/neo-gui;component/Resources/CustomAccentThemeResources.xaml"));


            // Try load custom theme
            var themeJson = Settings.Default.AppTheme;

            // Check if theme JSON has been set
            if (string.IsNullOrEmpty(themeJson))
            {
                SetThemeToDefault();
                return;
            }

            var theme = NeoGuiTheme.ImportFromJson(themeJson);

            if (theme == null)
            {
                theme = NeoGuiTheme.Default;
            }

            this.SetTheme(theme);
        }

        public void SetTheme(NeoGuiTheme newTheme)
        {
            // Change app style to the custom accent and current theme
            var accent = ThemeManager.GetAccent(CustomAccentKey);
            var theme = ThemeManager.GetAppTheme(newTheme.Style == ThemeStyle.Light ? "BaseLight" : "BaseDark");

            // Modify resource values to new theme values

            // Set accent colors
            if (accent.Resources.Contains(AccentBaseColorKey))
            {
                // Set base accent color
                accent.Resources[AccentBaseColorKey] = newTheme.AccentBaseColor.ToMediaColor();


                // Set other accent colors with reduced transparency

                // Set 80% transparency accent color
                if (accent.Resources.Contains(AccentColor1Key))
                {
                    accent.Resources[AccentColor1Key] = newTheme.AccentBaseColor.SetTransparencyFraction(0.8).ToMediaColor();
                }

                // Set 60% transparency accent color
                if (accent.Resources.Contains(AccentColor2Key))
                {
                    accent.Resources[AccentColor2Key] = newTheme.AccentBaseColor.SetTransparencyFraction(0.6).ToMediaColor();
                }

                // Set 40% transparency accent color
                if (accent.Resources.Contains(AccentColor3Key))
                {
                    accent.Resources[AccentColor3Key] = newTheme.AccentBaseColor.SetTransparencyFraction(0.4).ToMediaColor();
                }

                // Set 20% transparency accent color
                if (accent.Resources.Contains(AccentColor4Key))
                {
                    accent.Resources[AccentColor4Key] = newTheme.AccentBaseColor.SetTransparencyFraction(0.2).ToMediaColor();
                }
            }

            // Set highlight color
            if (accent.Resources.Contains(HighlightColorKey))
            {
                accent.Resources[HighlightColorKey] = newTheme.HighlightColor.ToMediaColor();
            }

            // Set window border color
            if (accent.Resources.Contains(WindowBorderColorKey))
            {
                accent.Resources[WindowBorderColorKey] = newTheme.WindowBorderColor.ToMediaColor();
            }

            ThemeManager.ChangeAppStyle(Application.Current, accent, theme);

            this.CurrentTheme = newTheme;
        }

        #endregion

        #region Private methods

        private void SetThemeToDefault()
        {
            this.SetTheme(NeoGuiTheme.Default);
        }

        #endregion
    }
}