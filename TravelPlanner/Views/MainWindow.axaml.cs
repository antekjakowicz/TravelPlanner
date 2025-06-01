using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace TravelPlanner.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    /*Przycisk do dodania miasta do listy*/
    private void AddCityButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var city = CityTextBox.Text;
        if (!string.IsNullOrWhiteSpace(city))
        {
            CitiesListBox.Items.Add(city);
            CityTextBox.Text = "";
        }
    }

    /*Zmiana obrazka powiazana z comboboxem*/
    private void CountryComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var selectedCountry = (CountryComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
        
        string imageFile = selectedCountry switch
        {
            "Włochy" => "italy.png",
            "Japonia" => "japan.jpg",
            "Norwegia" => "norway.jpg",
            "USA" => "usa.jpg",
            "Francja" => "france.jpg",
            _ => ""
        };
        
        if (!string.IsNullOrWhiteSpace(imageFile))
        {
            try
            {
                var uri = new Uri($"avares://TravelPlanner/Assets/Images/{imageFile}");
                using var stream = AssetLoader.Open(uri);
                CountryImage.Source = new Bitmap(stream);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Image error] {ex.Message}");
            }
        }
    }

    /*wywolanie tego nowego okienka z podsumowaniem*/
    private void ShowSummaryButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var name = NameTextBox.Text;
        var country = (CountryComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
        
        var attractions = new List<string>();
        if (MuseumsCheckBox.IsChecked == true) attractions.Add("Muzea");
        if (ParksCheckBox.IsChecked == true) attractions.Add("Parki Narodowe");
        if (MonumentsCheckBox.IsChecked == true) attractions.Add("Zabytki");
        if (RestaurantsCheckBox.IsChecked == true) attractions.Add("Restauracje");
        if (ArtGalleriesCheckBox.IsChecked == true) attractions.Add("Galerie Sztuki");
        if (FestivalsCheckBox.IsChecked == true) attractions.Add("Festiwale i koncerty");
        
        string transport = "";
        if (PlaneRadioButton.IsChecked == true) transport = "Samolot";
        else if (CarRadioButton.IsChecked == true) transport = "Samochód";
        else if (TrainRadioButton.IsChecked == true) transport = "Pociąg";
        else if (ShipRadioButton.IsChecked == true) transport = "Prom";
        
        var cities = CitiesListBox.Items.Cast<string>().ToList();

        var summaryWindow = new SummaryWindow(name, country, attractions, transport, cities);
        summaryWindow.ShowDialog(this);
    }
}