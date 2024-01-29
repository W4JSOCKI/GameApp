using GameAppMAUI.ViewModels;
namespace GameAppMAUI;

public partial class DevelopersPage : ContentPage
{
    private readonly DeveloperCollectionViewModel _viewModel;
    public DevelopersPage(DeveloperCollectionViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    public void OnSelectedDeveloper(object sender, ItemTappedEventArgs args)
    {
        _viewModel.OnSelectedDeveloper(args);
    }
}