using Microsoft.Maui.Controls;
using InterfazDb.Helpers;

namespace InterfazDb.Pages;

public partial class InsertPersonPage : ContentPage
{
    public InsertPersonPage()
    {
        InitializeComponent();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NombreEntry.Text))
        {
            await DisplayAlert("Error", "Please enter a name", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(CargoEntry.Text))
        {
            await DisplayAlert("Error", "Please enter a position", "OK");
            return;
        }

        try
        {
            // Create the SQL insert statement
            string sql = $"INSERT INTO CatPersonal (Nombre, Cargo) VALUES ('{NombreEntry.Text}', '{CargoEntry.Text}')";
            
            // Execute the SQL statement using DatabaseModify
            DatabaseModify.ExecuteSql(sql);
            
            await DisplayAlert("Success", "Person saved successfully!", "OK");
            
            // Clear the entries
            NombreEntry.Text = string.Empty;
            CargoEntry.Text = string.Empty;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to save person: {ex.Message}", "OK");
        }
    }
} 