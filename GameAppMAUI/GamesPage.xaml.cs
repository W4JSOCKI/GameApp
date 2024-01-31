namespace GameAppMAUI;
using GameAppMAUI.ViewModels;


public partial class GamesPage : ContentPage
{
    private readonly GamePageViewModel _viewModel;
    public GamesPage(GamePageViewModel viewModel)
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
