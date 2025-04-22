using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;
using InterfazDb.Helpers;
using InterfazDb.Models;
using System.Data;

namespace InterfazDb.Pages;

public partial class InsertInventoryPage : ContentPage
{
    private readonly DatabaseGetData _database;
    private List<Product> _products;
    private Product _selectedProduct;

    public InsertInventoryPage()
    {
        InitializeComponent();
        _database = new DatabaseGetData();
        _products = new List<Product>();
        
        // Call LoadProducts after the page is loaded
        this.Loaded += async (s, e) => await LoadProducts();
    }

    private async Task LoadProducts()
    {
        try
        {
            string query = "SELECT ID, Descripcion FROM CatProducto";
            DataTable productsTable = await _database.ExecuteSelectQueryAsync(query);
            
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                if (productsTable == null || productsTable.Rows.Count == 0)
                {
                    return;
                }

                _products.Clear();
                foreach (DataRow row in productsTable.Rows)
                {
                    _products.Add(new Product
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        Descripcion = row["Descripcion"].ToString()
                    });
                }
            });
        }
        catch (Exception ex)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await DisplayAlert("Error", $"Failed to load products: {ex.Message}", "OK");
            });
        }
    }

    private async void OnSelectProductClicked(object sender, EventArgs e)
    {
        if (_products == null || !_products.Any())
        {
            await DisplayAlert("Error", "No products available", "OK");
            return;
        }

        string[] productNames = _products.Select(p => p.Descripcion).ToArray();
        string action = await DisplayActionSheet("Select a product", "Cancel", null, productNames);
        
        if (action != "Cancel")
        {
            _selectedProduct = _products.FirstOrDefault(p => p.Descripcion == action);
            if (_selectedProduct != null)
            {
                SelectedProductLabel.Text = _selectedProduct.Descripcion;
            }
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (_selectedProduct == null)
        {
            await DisplayAlert("Error", "Please select a product", "OK");
            return;
        }

        if (!int.TryParse(CantidadEntry.Text, out int quantity))
        {
            await DisplayAlert("Error", "Please enter a valid quantity", "OK");
            return;
        }

        try
        {
            string sql = $"INSERT INTO tblInventario (IDProducto, Cantidad) VALUES ({_selectedProduct.ID}, {quantity})";
            DatabaseModify.ExecuteSql(sql);
            
            await DisplayAlert("Success", "Inventory record saved successfully!", "OK");
            
            // Clear the entries
            _selectedProduct = null;
            SelectedProductLabel.Text = "No product selected";
            CantidadEntry.Text = string.Empty;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to save inventory: {ex.Message}", "OK");
        }
    }
} 