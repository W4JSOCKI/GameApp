using GameAppMAUI.ViewModels;

namespace GameAppMAUI;
public partial class GameAddPage : ContentPage
{
	public GameAddPage(GamePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}