using GameAppMAUI.ViewModels;
namespace GameAppMAUI;

public partial class DeveloperEditPage : ContentPage
{
	public DeveloperEditPage(DevPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}