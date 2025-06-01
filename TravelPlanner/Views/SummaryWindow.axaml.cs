using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TravelPlanner.Views;

public partial class SummaryWindow : Window
{
    private string travelerName;
    private string country;
    private string transport;
    private List<string> attractionsList = new();
    private List<string> citiesList = new();

    public SummaryWindow(string travelerName, string country, List<string> attractions, string transport, List<string> cities)
    {
        InitializeComponent();
        
        /*Dodanie danych do axamlowych pol*/
        TravelerNameTextBlock.Text = $"👤 Podróżujący: {travelerName}";
        CountryTextBlock.Text = $"🌍 Kraj docelowy: {country}";
        TransportTextBlock.Text = $"🚗 Transport: {transport}";
        AttractionsTextBlock.Text = $"🏞️ Atrakcje: {string.Join(", ", attractions)}";
        CitiesTextBlock.Text = $"🏙️ Miasta do odwiedzenia: {string.Join(", ", cities)}";
        
        /*pola klas w konstruktorze do zapisu w pliku*/
        this.travelerName = travelerName;
        this.country = country;
        this.transport = transport;
        this.attractionsList = attractions;
        this.citiesList = cities;
    }

    /*Zapis podsumowania do pliku*/
    private void SaveToFileButton_Click(object? sender, RoutedEventArgs e)
    {
        string summaryText = GenerateSummaryText();
        string filePath = "summary.txt";

        try
        {
            File.WriteAllText(filePath, summaryText);
            Console.WriteLine("Zapisano do pliku: "+filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    /*Wygenerowanie tekstu do pliku txt*/
    private string GenerateSummaryText()
    {
        return string.Join(Environment.NewLine, new[]
        {
            "📋 PODSUMOWANIE PODRÓŻY",
            "",
            $"👤 Podróżujący: {travelerName}",
            $"🌍 Kraj docelowy: {country}",
            $"🚗 Transport: {transport}",
            $"🏞️ Atrakcje: {string.Join(", ", attractionsList)}",
            $"🏙️ Miasta do odwiedzenia: {string.Join(", ", citiesList)}",
            ""
        });
    }
}