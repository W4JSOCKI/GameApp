using GameAppMAUI.ViewModels;
namespace GameAppMAUI;

public partial class DeveloperEditPage : ContentPage
{
	public DeveloperEditPage(DeveloperCollectionViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}