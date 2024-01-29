using GameAppMAUI.ViewModels;

namespace GameAppMAUI;

public partial class DeveloperAddPage : ContentPage
{
	public DeveloperAddPage(DeveloperCollectionViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}