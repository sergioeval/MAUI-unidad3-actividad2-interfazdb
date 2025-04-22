using Microsoft.Maui.Controls;

namespace InterfazDb.Pages;

public partial class InsertPersonPage : ContentPage
{
    public InsertPersonPage()
    {
        InitializeComponent();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text))
        {
            await DisplayAlert("Error", "Please enter a name", "OK");
            return;
        }

        // TODO: Add your database insertion logic here
        await DisplayAlert("Success", "Person saved successfully!", "OK");
        
        // Clear the entries
        NameEntry.Text = string.Empty;
        EmailEntry.Text = string.Empty;
        PhoneEntry.Text = string.Empty;
    }
} 