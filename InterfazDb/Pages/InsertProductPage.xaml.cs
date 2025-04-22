using Microsoft.Maui.Controls;
using InterfazDb.Helpers;
using System.Data;
using System.Diagnostics;

namespace InterfazDb.Pages;

public partial class InsertProductPage : ContentPage
{
    private readonly DatabaseGetData _database;

    public InsertProductPage()
    {
        InitializeComponent();
        _database = new DatabaseGetData();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(DescripcionEntry.Text))
        {
            await DisplayAlert("Error", "Please enter a product description", "OK");
            return;
        }

        try
        {
            string sql = $"INSERT INTO CatProducto (Descripcion) VALUES ('{DescripcionEntry.Text}')";
            DatabaseModify.ExecuteSql(sql);
            await DisplayAlert("Success", "Product saved successfully!", "OK");
            
            // Clear the entries
            DescripcionEntry.Text = string.Empty;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to save product: {ex.Message}", "OK");
        }
    }

    private async void OnShowProductsClicked(object sender, EventArgs e)
    {
        try
        {
            string query = "SELECT ID, Descripcion FROM CatProducto";
            DataTable products = await _database.ExecuteSelectQueryAsync(query);
            
            // Debug information
            string debugInfo = $"Rows found: {products.Rows.Count}\n";
            if (products.Rows.Count > 0)
            {
                debugInfo += "First row data:\n";
                foreach (DataColumn col in products.Columns)
                {
                    debugInfo += $"Column: {col.ColumnName}, Value: {products.Rows[0][col.ColumnName]}\n";
                }
            }
            DebugLabel.Text = debugInfo;
            DebugLabel.IsVisible = true;

            if (products.Rows.Count > 0)
            {
                ProductsCollectionView.ItemsSource = products.DefaultView;
                ProductsCollectionView.IsVisible = true;
            }
            else
            {
                await DisplayAlert("Info", "No products found in the database", "OK");
            }
        }
        catch (Exception ex)
        {
            DebugLabel.Text = $"Error: {ex.Message}";
            DebugLabel.IsVisible = true;
            await DisplayAlert("Error", $"Failed to load products: {ex.Message}", "OK");
        }
    }
} 