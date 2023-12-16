using Trif_Andrei_Lab7.Data;
using Trif_Andrei_Lab7.Models;

namespace Trif_Andrei_Lab7;

public partial class ListPage : ContentPage
{
    public ListPage()
    {
        InitializeComponent();
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)BindingContext) { BindingContext = new Product() });
    }

    async void OnRemoveButtonClicked(object sender, EventArgs e)
    {
        var shopList = (ShopList)BindingContext;
        
        if (listView.SelectedItem != null)
        {
            var product = (Product)listView.SelectedItem;

            var listProduct = new ListProduct()
            {
                ShopListID = shopList.ID,
                ProductID = product.ID
            };

            await App.Database.DeleteListProductAsync(listProduct);

            await LoadListProducts(shopList.ID);
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var shopl = (ShopList)BindingContext;

        await LoadListProducts(shopl.ID);
    }

    private async Task LoadListProducts(int shopListID)
    {
        var products = await App.Database.GetListProductsAsync(shopListID);

        listView.ItemsSource = products;
    }
}