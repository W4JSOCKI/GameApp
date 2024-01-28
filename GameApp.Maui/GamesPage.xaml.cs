namespace Wojtalak_Szczerkowski.GameApp.Maui;
using Wojtalak_Szczerkowski.GameApp.Maui.ViewModels;

public partial class GamesPage : ContentPage
{
    public GamesPage(GamesCollectionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}