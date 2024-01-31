using GameAppMAUI.ViewModels;
namespace GameAppMAUI;

public partial class GameEditPage : ContentPage
{
	public GameEditPage(GamePageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}