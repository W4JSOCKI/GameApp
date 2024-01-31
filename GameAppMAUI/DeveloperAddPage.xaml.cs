using GameAppMAUI.ViewModels;

namespace GameAppMAUI;
public partial class DeveloperAddPage: ContentPage
{
    public DeveloperAddPage(DevPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}