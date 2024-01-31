using GameAppMAUI.ViewModels;
namespace GameAppMAUI;

public partial class DevelopersPage : ContentPage
{
    private readonly DevPageViewModel _viewModel;
    public DevelopersPage(DevPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    public void EditRedirect(object sender, ItemTappedEventArgs args)
    {
        _viewModel.EditRedirect(args);
    }
}