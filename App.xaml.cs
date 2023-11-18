using Trif_Andrei_Lab7.Data;

namespace Trif_Andrei_Lab7;

public partial class App : Application
{
    static ShoppingListDatabase database;

    public static ShoppingListDatabase Database
    {
        get
        {
            database ??= new ShoppingListDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ShoppingList.db3"));
            return database;
        }
    }

    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
