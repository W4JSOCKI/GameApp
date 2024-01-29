namespace GameAppMAUI;
using GameAppMAUI.ViewModels;


public partial class GamesPage : ContentPage
{
    private readonly GameCollectionViewModel _viewModel;
    public GamesPage(GameCollectionViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }
    public void OnSelectedGame(object sender, ItemTappedEventArgs args)
    {
        _viewModel.OnSelectedGame(args);
    }
}
