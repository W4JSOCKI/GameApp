using CommunityToolkit.Mvvm.ComponentModel;
using Wojtalak_Szczerkowski.GameApp.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GameAppMAUI.ViewModels
{
    public partial class DevViewModel : ObservableValidator, IDeveloper
    {
        [ObservableProperty]
        [Required]
        private int id;

        [ObservableProperty]
        [MinLength(1)]
        [Required]
        private string name;

        [ObservableProperty]
        [Required]
        private string country;

        public DevViewModel() { }
        public DevViewModel(IDeveloper developer)
        {
            id = developer.Id;
            name = developer.Name;
            country = developer.Country;
        }
      
    }
}
