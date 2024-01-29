using GamesAppMaui.ViewModels;
namespace GamesAppMaui;


public partial class GamesPage : ContentPage
{
	public GamesPage(GamesCollectionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}