using System.Collections.Generic;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace TravelPlanner.Views;

public partial class SummaryWindow : Window
{
    public SummaryWindow(string travelerName, string country, List<string> attractions, string transport, List<string> cities)
    {
        InitializeComponent();
        BuildSummary(travelerName, country, attractions, transport, cities);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void BuildSummary(string travelerName, string country, List<string> attractions, string transport,
        List<string> cities)
    {
        var panel = this.FindControl<Panel>("SummaryStackPanel");
        
        panel.Children.Add(new TextBlock { Text = $"👤 Podróżujący: {travelerName}" });
        panel.Children.Add(new TextBlock { Text = $"🌍 Kraj docelowy: {country}" });
        panel.Children.Add(new TextBlock { Text = $"🚗 Transport: {transport}" });

        panel.Children.Add(new TextBlock { Text = $"🏞️ Atrakcje: {string.Join(", ", attractions)}" });
        panel.Children.Add(new TextBlock { Text = $"🏙️ Miasta do odwiedzenia: {string.Join(", ", cities)}" });
    }
    
    private void CloseButton_Click(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}