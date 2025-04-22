using InterfazDb.Pages;

namespace InterfazDb;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        // Register routes for all pages
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(InsertPersonPage), typeof(InsertPersonPage));
        Routing.RegisterRoute(nameof(InsertProductPage), typeof(InsertProductPage));
    }
}