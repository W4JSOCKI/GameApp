namespace GameAppMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();


            Routing.RegisterRoute(nameof(GameAddPage), typeof(GameAddPage));
            Routing.RegisterRoute(nameof(GameEditPage), typeof(GameEditPage));
            Routing.RegisterRoute(nameof(DeveloperAddPage), typeof(DeveloperAddPage));
            Routing.RegisterRoute(nameof(DeveloperEditPage), typeof(DeveloperEditPage));
        }
    }
}
