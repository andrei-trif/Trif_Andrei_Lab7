using Trif_Andrei_Lab7.Data;
using Trif_Andrei_Lab7.Models;

namespace Trif_Andrei_Lab7;

public partial class ProductPage : ContentPage
{
    ShopList _slist;

    public ProductPage(ShopList slist)
    {
        InitializeComponent();
        _slist = slist;
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var product = (Product)BindingContext;

        await App.Database.SaveProductAsync(product);

        listView.ItemsSource = await App.Database.GetProductsAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var product = (Product)BindingContext;

        await App.Database.DeleteProductAsync(product);

        listView.ItemsSource = await App.Database.GetProductsAsync();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        listView.ItemsSource = await App.Database.GetProductsAsync();
    }

    async void OnAddButtonClicked(object sender, EventArgs e)
    {
        Product p;

        if (listView.SelectedItem != null)
        {
            p = listView.SelectedItem as Product;

            var lp = new ListProduct()
            {
                ShopListID = _slist.ID,
                ProductID = p.ID
            };

            await App.Database.SaveListProductAsync(lp);

            p.ListProducts = new List<ListProduct> { lp };

            await Navigation.PopAsync();
        }
    }
}