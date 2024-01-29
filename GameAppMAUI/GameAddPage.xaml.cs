using GameAppMAUI.ViewModels;

namespace GameAppMAUI;
public partial class GameAddPage : ContentPage
{
	public GameAddPage(GameCollectionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}