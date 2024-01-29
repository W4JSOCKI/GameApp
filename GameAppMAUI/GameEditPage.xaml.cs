using GameAppMAUI.ViewModels;
namespace GameAppMAUI;

public partial class GameEditPage : ContentPage
{
	public GameEditPage(GameCollectionViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}