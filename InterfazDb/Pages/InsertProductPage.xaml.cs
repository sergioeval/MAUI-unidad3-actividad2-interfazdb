using Microsoft.Maui.Controls;

namespace InterfazDb.Pages;

public partial class InsertProductPage : ContentPage
{
    public InsertProductPage()
    {
        InitializeComponent();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ProductNameEntry.Text))
        {
            await DisplayAlert("Error", "Please enter a product name", "OK");
            return;
        }

        if (!decimal.TryParse(PriceEntry.Text, out decimal price))
        {
            await DisplayAlert("Error", "Please enter a valid price", "OK");
            return;
        }

        if (!int.TryParse(StockEntry.Text, out int stock))
        {
            await DisplayAlert("Error", "Please enter a valid stock quantity", "OK");
            return;
        }

        // TODO: Add your database insertion logic here
        await DisplayAlert("Success", "Product saved successfully!", "OK");
        
        // Clear the entries
        ProductNameEntry.Text = string.Empty;
        DescriptionEntry.Text = string.Empty;
        PriceEntry.Text = string.Empty;
        StockEntry.Text = string.Empty;
    }
} 